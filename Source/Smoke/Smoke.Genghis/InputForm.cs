using CompanyName.ProductName.Domain;
using CompanyName.ProductName.Provider.Betfair;
using Smoke.DataFeed.Coordinator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smoke.Genghis
{
    public partial class inputForm : Form, IObserver<Dictionary<int, ExchangePrice>>
    {
        private const string ProviderName = "betfair";
        private int _matchId;
        private int _chosenRunnerId;
        private decimal _chosenOdds;
        private decimal _chosenAmount;
        private decimal _budget;
        private DateTime _expectedEndingTime;
        private bool _stopProcessing = false;

        private Dictionary<string, Runner> _runnersIndexedById;
        private BetfairMarketProvider _provider = new BetfairMarketProvider();

        public inputForm()
        {
            InitializeComponent();
        }

        #region IObserver methods
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

        private void ProcessUpdatedOdds(Dictionary<int, ExchangePrice> value)
        {
            OptimumValues optimumValues = Optimiser.Optimise(value, _chosenRunnerId, _chosenOdds, _chosenAmount, _budget);

            if(DecisionEngine.PlaceBet(optimumValues.ChosenAlternativeAmount, optimumValues.WorstPayoff, _expectedEndingTime))
            {
                if (ExecutionEngine.PlaceBets(_provider, _matchId, optimumValues.Alternative1Id, optimumValues.Alternative1Amount,
                    optimumValues.Alternative1Odds, optimumValues.Alternative2Id, optimumValues.Alternative2Amount, optimumValues.Alternative2Odds))
                {
                    _stopProcessing = true;
                }
            }
            // fill in the gaps... those values which were not populated by optimiser.
            optimumValues.Alternative1Name = _runnersIndexedById[optimumValues.Alternative1Id.ToString()].Title;
            optimumValues.Alternative2Name = _runnersIndexedById[optimumValues.Alternative2Id.ToString()].Title;
            optimumValues.ChosenAlternativeName = _runnersIndexedById[optimumValues.ChosenAlternativeId.ToString()].Title;
            // inform listeners if this needs to be informed
            Informer.Inform(optimumValues);
        }
        #endregion

        private void startButton_Click(object sender, EventArgs e)
        {
            
            InitialiseData();
            PriceWatcher.Instance.Subscribe(this);
            PriceWatcher.Instance.Start(_matchId);
        }

        private void getMarketsButton_Click(object sender, EventArgs e)
        {
            BetfairMarketProvider provider = new BetfairMarketProvider();
            List<FootballMatch> markets = provider.GetFootballMarkets(ProviderName);
            markets.ForEach(m =>
                {
                    if (m.Title.ToLower().Contains("liverpool"))
                    {
                        MessageBox.Show(m.Id.ToString());
                    }
                });
            List<Runner> runners = provider.GetRunnersForMarket(ProviderName, "108051706");
        }

        private void InitialiseData()
        {
            _matchId = int.Parse(matchIdTextBox.Text.Trim());
            _chosenRunnerId = int.Parse(chosenRunnerIdTextBox.Text.Trim());
            _chosenOdds = decimal.Parse(chosenOddsTextBox.Text.Trim());
            _chosenAmount = decimal.Parse(chosenAmountTextBox.Text.Trim());
            _budget = decimal.Parse(budgetTextBox.Text.Trim());
            _expectedEndingTime = expectedEndingDateTimePicker.Value;

            List<Runner> runners = _provider.GetRunnersForMarket(ProviderName, _matchId.ToString());
            _runnersIndexedById = new Dictionary<string, Runner>();
            foreach (Runner runner in runners)
            {
                _runnersIndexedById.Add(runner.Id, runner);
            }
        }
        #region Optimiser
        
        #endregion
        private void Alert(OptimumValues optimumValues)
        {
            // check if a notification should be sent and then send notification.
        }
    }
}
