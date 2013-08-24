using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyName.ProductName.Provider.Betfair;
using CompanyName.ProductName.Domain;
using Smoke.DataFeed.Coordinator;
using System.ServiceModel;
using Smoke.Domain;
using Smoke.Utilities.Aliases;
using Smoke.DataFeed.LiveScores;

namespace Smoke.Test
{
    public partial class Form1 : Form, IObserver<object>
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void betfairServiceTest_Click(object sender, EventArgs e)
        {
            BetfairMarketProvider provider = new BetfairMarketProvider();
            List<FootballMatch> markets = provider.GetFootballMarkets("betfair");
            int marketId = 0;
            foreach (FootballMatch market in markets)
            {
                if (market.Title.ToLower().Contains("bristol"))
                {
                    MessageBox.Show(market.Id);
                    marketId = int.Parse(market.Id);
                }
            }

            List<Runner> runners = provider.GetRunnersForMarket("betfair", marketId.ToString());
        }

        private void priceWatcherTestButton_Click(object sender, EventArgs e)
        {
            PriceWatcher.Instance.Subscribe(this);
            int marketId = 108103694;
            PriceWatcher.Instance.Start(marketId);
        }

        #region IObserver methods
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
        public void OnNext(object value)
        {
            MessageBox.Show("Next value received.");
        }
        #endregion

        private void placeBetButton_Click(object sender, EventArgs e)
        {
            BetfairMarketProvider provider = new BetfairMarketProvider();
            List<Market> cricketMarkets = provider.GetCricketMarkets("betfair");
            BetToPlace bet1 = new BetToPlace()
            {
                Amount = 10.0M,
                MarketId = 108104763,
                Price = 2.58M,
                RunnerId = 69718
            };
            provider.PlaceBets("betfair", new List<BetToPlace>() { bet1 });
            //List<Runner> runners = provider.GetRunnersForMarket("betfair", "107947974");
        }

        private void wcfTestButton_Click(object sender, EventArgs e)
        {
            IBettingExchangeService dataFeedService = null;
            var dataFeedtAddress = new EndpointAddress("net.tcp://localhost:8585/BetfairService");
            dataFeedService = ChannelFactory<IBettingExchangeService>.CreateChannel(new NetTcpBinding(), dataFeedtAddress);
            List<FootballMatch> markets = dataFeedService.GetFootballMarkets("betfair");
        }

        private void scoreProTestButton_Click(object sender, EventArgs e)
        {
            ScoresProApi.Instance.GetLatestFootballScore("Wofoo Tai Po", "Yokohama Hong Kong");
        }

        private void manageRunnerAliasesButton_Click(object sender, EventArgs e)
        {
            RunnerAliasesForm form = new RunnerAliasesForm();
            form.Show();
        }
    }
}
