using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.ProductName.Domain;

namespace CompanyName.ProductName.Provider.Betfair
{
    public class BetfairMarketProvider : MarketProviderBase, IDisposable
    {
        public const string ProviderName = "betfair";
        public override string Name
        {
            get { return ProviderName; }
        }


        // Track whether Dispose has been called.
        private bool _disposed = false;

        public BetfairMarketProvider()
        {
            
        }

        public override List<FootballMatch> GetFootballMarkets(string provider)
        {
            if (string.IsNullOrEmpty(provider))
                throw new ArgumentNullException("provider", "provider is null");

            EnsureCorrectProvider(provider);

            List<BetfairMarket> betfairMarkets = BetfairApi.Instance.GetFootballMarkets();


            List<FootballMatch> markets = new List<FootballMatch>();
            betfairMarkets.ForEach(bm => 
            {
                FootballMatch market = new FootballMatch();
                market.Id = bm.Id.ToString(CultureInfo.InvariantCulture);
                market.Provider = this.Name;                
                market.Title = bm.Title;
                markets.Add(market);
            });

            return markets;
        }

        public override List<FootballMatch> GetFootballMarketsForCategory(string provider, string category)
        {
            if (string.IsNullOrEmpty(provider))
                throw new ArgumentNullException("provider", "provider is null");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("category", "category is null");

            EnsureCorrectProvider(provider);

            throw new NotImplementedException();
        }

        public override List<FootballMatch> GetFootballMarketsForLeague(string provider, string category, string league)
        {
            if (string.IsNullOrEmpty(provider))
                throw new ArgumentNullException("provider", "provider is null");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("category", "category is null");
            if (string.IsNullOrEmpty(league))
                throw new ArgumentNullException("league", "league is null");

            EnsureCorrectProvider(provider);


            throw new NotImplementedException();
        }

        public override List<Runner> GetRunnersForMarket(string provider, string marketId)
        {
            EnsureCorrectProvider(provider);

            // First we need to get market information since this 
            // contains the list of runners with names and ids.
            int betFairMarketId = int.Parse(marketId.Trim());
            BetfairMarketInfo marketInfo = BetfairApi.Instance.GetMarketInfo(betFairMarketId);

            List<Runner> runners = new List<Runner>();
            foreach (BetfairRunner betfairRunner in marketInfo.Runners)
            {
                Runner runner = new Runner();
                runner.Id = betfairRunner.Id.ToString(CultureInfo.InvariantCulture);
                runner.Provider = this.Name;
                runner.Title = betfairRunner.Name;

                // Commented following two lines after separating price from runner. Created RunnerWithPrice instead which inherits
                // from Runner class.
                // BetfairRunnerOddsInfo befairOddInfo = oddsIndexedByRunner[betfairRunner.Id];
                // runner.Price = new Price(befairOddInfo.Odds);
                runners.Add(runner);
            }

            return runners;
        }

        public override List<Runner> GetRunnersForMarket(string provider, string category, string league, string marketId)
        {
            if (string.IsNullOrEmpty(provider))
                throw new ArgumentNullException("provider", "provider is null");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("category", "category is null");
            if (string.IsNullOrEmpty(league))
                throw new ArgumentNullException("league", "league is null");
            if (string.IsNullOrEmpty(marketId))
                throw new ArgumentNullException("marketId", "marketId is null");

            EnsureCorrectProvider(provider);


            // First we need to get market information since this 
            // contains the list of runners with names and ids.
            int betFairMarketId = int.Parse(marketId);
            BetfairMarketInfo marketInfo = BetfairApi.Instance.GetMarketInfo(betFairMarketId);

            List<Runner> runners = new List<Runner>();
            foreach (BetfairRunner betfairRunner in marketInfo.Runners)
            {
                Runner runner = new Runner();
                runner.Provider = betfairRunner.Id.ToString(CultureInfo.InvariantCulture);
                runner.Title = betfairRunner.Name;

                // Commented following two lines after separating price from runner. Created RunnerWithPrice instead which inherits
                // from Runner class.
                // BetfairRunnerOddsInfo befairOddInfo = oddsIndexedByRunner[betfairRunner.Id];
                // runner.Price = new Price(befairOddInfo.Odds);
                runners.Add(runner);
            }

            return runners;
        }

        /// <summary>
        /// Gets odds indexed by runner id for the given market id.
        /// </summary>
        /// <param name="marketId">The market id for which to get odds.</param>
        /// <returns></returns>
        public Dictionary<int, ExchangePrice> GetOddsIndexedByRunnerId(int marketId)
        {
            return BetfairApi.Instance.GetOddsIndexByRunner(marketId);
            /*
            GetMarketPricesCompressedReq request = new GetMarketPricesCompressedReq();
            request.header = ConnectionManager.RequestHeader.ExchangeRequestHeader;
            request.marketId = marketId;
            GetMarketPricesCompressedResp response = ConnectionManager.GetMarketPricesCompressed(request);
            return Parser.GetOddsIndexedByRunner(response.marketPrices);
             */
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                try
                {
                    if (disposing)
                    {
                        // Release the managed resources you added in
                        // this derived class here.
                        if (BetfairApi.Instance != null)
                        {
                            BetfairApi.Instance.Dispose();
                        }
                    }
                    // Release the native unmanaged resources you added
                    // in this derived class here.
                    this._disposed = true;
                }
                finally
                {
                    // Call Dispose on your base class.
                    base.Dispose(disposing);
                }
            }
        }
    }
}
