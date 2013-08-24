using CompanyName.ProductName.Domain;
using Smoke.DataFeed.Coordinator;
using Smoke.Domain;
using Smoke.Tamerlane.Properties;
using Smoke.Utilities;
using Smoke.Utilities.Aliases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Tamerlane
{
    public class Manager : IObserver<Dictionary<int, ExchangePrice>>, IObserver<FootballScore>
    {
        private const string ProviderName = "betfair";
        private bool _stopProcessing = false;
        private string _matchName;
        private Dictionary<string, Runner> _runnersIndexedById;
        private IBettingExchangeService _provider = null;
        private string _alt1RunnerId, _alt2RunnerId;
        // NOTE: Tamerlane assumes that draw is never a chosen runner. so the three runners are: chosen, opponent and draw.
        // chosen runner name and opponent runner name terminology is used to be able to interpret football scores.
        private string _chosenRunnerName, _opponentRunnerName;
        private int _chosenRunnerScore = 0, _opponentRunnerScore = 0;

        private object _syncLock = new object();
        private volatile FootballScore _latestScore = new FootballScore();
        private FootballScore LatestScore
        {
            get
            {
                return _latestScore;
            }
            set
            {
                lock (_syncLock)
                {
                    _latestScore = value;
                }
            }
        }

        public string MarketId { get; set; }
        public string ChosenRunnerId { get; set; }
        public decimal ChosenOdds { get; set; }
        public decimal ChosenAmount { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartingTime { get; set; }

        #region IObserver (Prices) methods
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
        public void OnNext(Dictionary<int, ExchangePrice> value)
        {
            if (!_stopProcessing)
            {
                ProcessUpdatedOdds(value);
            }
        }
        #endregion

        #region IObserver (Live Scores) methods
        public void OnNext(FootballScore value)
        {
            if (!_stopProcessing)
            {
                ProcessUpdatedScore(value);
            }
        }
        #endregion

        public void Start()
        {
            InitialiseData();
            // subscribe to price watcher
            PriceWatcher.Instance.Subscribe(this);
            PriceWatcher.Instance.Start(int.Parse(MarketId));
            // subscribe to scores watcher
            ScoresWatcher.Instance.Subscribe(this);
            ScoresWatcher.Instance.Start(_chosenRunnerName, _opponentRunnerName);
        }
        
        private void InitialiseData()
        {
            InitialiseBettingExchangeService();

            List<Runner> runners = _provider.GetRunnersForMarket(ProviderName, MarketId);
            _runnersIndexedById = new Dictionary<string, Runner>();

            // the loop does three things: 1. creates the dictionary 2. initialises alt1RunnerId and alt2RunnerId 3. composes match name
            _alt1RunnerId = _alt2RunnerId = string.Empty;
            _matchName = string.Empty;
            string alt1Name = string.Empty, alt2Name = string.Empty;
            foreach (Runner runner in runners)
            {
                _runnersIndexedById.Add(runner.Id, runner);
                if (runner.Id != ChosenRunnerId)
                {
                    if (string.IsNullOrEmpty(_alt1RunnerId))
                    {
                        _alt1RunnerId = runner.Id;
                        alt1Name = runner.Title;
                        if (alt1Name != "The Draw")
                        {
                            _opponentRunnerName = RunnerAliasesHelper.GetNameAliasesForAlias(alt1Name).RunnerName;
                        }
                    }
                    else
                    {
                        _alt2RunnerId = runner.Id;
                        alt2Name = runner.Title;
                        if (alt2Name != "The Draw")
                        {
                            _opponentRunnerName = alt2Name;
                        }
                    }
                }
                else
                {
                    _chosenRunnerName = RunnerAliasesHelper.GetNameAliasesForAlias(runner.Title).RunnerName;
                }
                _matchName += runner.Title + "-";
            }
            _matchName = _matchName.Substring(0, _matchName.Length - 1);
            Logger.Write(Resources.ProjectName, _matchName, "Initialising.");
            Logger.Write(Resources.ProjectName, _matchName, "Alt1: " + alt1Name + "; Alt2: " + alt2Name);
            Logger.Write(Resources.ProjectName, _matchName, "Chosen Odds: " + ChosenOdds);
        }

        private void InitialiseBettingExchangeService()
        {
            if (_provider == null)
            {
                EndpointAddress betfairServiceAddress = new EndpointAddress("net.tcp://localhost:8585/BetfairService");
                _provider = ChannelFactory<IBettingExchangeService>.CreateChannel(new NetTcpBinding(), betfairServiceAddress);
            }
        }

        private void ProcessUpdatedOdds(Dictionary<int, ExchangePrice> value)
        {
            Logger.Write(Resources.ProjectName, _matchName, "*************************Updated Odds Received. Starting Processing.*********************");
            // log everything...
            decimal alt1Odds = value[int.Parse(_alt1RunnerId)].Odds;
            Logger.Write(Resources.ProjectName, _matchName, "Alt1 Odds: " + alt1Odds.ToString());
            decimal alt2Odds = value[int.Parse(_alt2RunnerId)].Odds;
            Logger.Write(Resources.ProjectName, _matchName, "Alt2 Odds: " + alt2Odds.ToString());
            // 1. get rfp from compute engine
            // OkashTODO: all runner ids should be string fields. the dictionary 'value' should be <string, ExchangePrice>. 
            Recommendation recommendation =
                ComputeEngine.GetRecommendation(ChosenAmount, ChosenOdds, alt1Odds, alt2Odds, Budget, _matchName);
            Logger.Write(Resources.ProjectName, _matchName, "Counter Bet Amount= " + recommendation.CounterBetAmount);
            Logger.Write(Resources.ProjectName, _matchName, "Risk Free Payoff= " + recommendation.RiskFreePayoff);
            Logger.Write(Resources.ProjectName, _matchName, "Alt1 Amount= " + recommendation.Alt1Amount);
            Logger.Write(Resources.ProjectName, _matchName, "Alt2 Amount= " + recommendation.Alt2Amount);
            // 2. pass rfp and exp end time to decision engine. if decision engine returns true then
            if (DecisionEngine.PlaceBet(ChosenAmount, recommendation.RiskFreePayoff, StartingTime, LatestScore))
            {
                Logger.Write(Resources.ProjectName, _matchName, "Place Bet Returned True");

                // 4. place bets using execution engine
                if (ExecutionEngine.PlaceBets(_provider, int.Parse(MarketId), int.Parse(_alt1RunnerId), recommendation.Alt1Amount, alt1Odds,
                    int.Parse(_alt2RunnerId), recommendation.Alt2Amount, alt2Odds))
                {
                    Logger.Write(Resources.ProjectName, _matchName, "Placed Bet Successfully");
                    // 5. set stop processing to true
                    _stopProcessing = true;
                }
            }
            Logger.Write(Resources.ProjectName, _matchName, "*************************Ending Processing.*********************");
        }

        private void ProcessUpdatedScore(FootballScore newScore)
        {
            LatestScore = newScore;
            // OkashTODO: extract chosen runner score and opponent score.
            if (newScore.HomeName == _chosenRunnerName)
            {
                _chosenRunnerScore = newScore.HomeScore;
                _opponentRunnerScore = newScore.AwayScore;
            }
            else
            {
                _chosenRunnerScore = newScore.AwayScore;
                _opponentRunnerScore = newScore.HomeScore;
            }
            Logger.Write(Resources.ProjectName, _matchName, "----New Score Received. [" + newScore.ToString() + "]");
        }
    }
}
