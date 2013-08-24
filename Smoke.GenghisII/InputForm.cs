using CompanyName.ProductName.Domain;
using Smoke.DataFeed.Coordinator;
using Smoke.Domain;
using Smoke.GenghisII.Properties;
using Smoke.Utilities;
using Smoke.Utilities.Aliases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smoke.GenghisII
{
    public partial class InputForm : Form
    {
        private const string ProviderName = "betfair";
        private string _matchName;
        private int _matchId;
        private string _chosenRunnerId;
        private decimal _chosenOdds;
        private decimal _chosenAmount;
        private decimal _budget;
        private DateTime _startingTime;
        private Dictionary<string, Runner> _runnersIndexedById;
        private IBettingExchangeService _provider = null;
        private string _alt1RunnerId, _alt2RunnerId;
        

        public InputForm()
        {
            InitializeComponent();
        }
       
        private void InitialiseBettingExchangeService()
        {
            if (_provider == null)
            {
                EndpointAddress betfairServiceAddress = new EndpointAddress("net.tcp://localhost:8585/BetfairService");
                _provider = ChannelFactory<IBettingExchangeService>.CreateChannel(new NetTcpBinding(), betfairServiceAddress);
            }
        }
        private void InitialiseData()
        {
            InitialiseBettingExchangeService();
            _matchId = int.Parse(matchIdTextBox.Text.Trim());
            _chosenRunnerId = chosenRunnerIdTextBox.Text.Trim();
            _chosenOdds = decimal.Parse(chosenOddsTextBox.Text.Trim());
            _chosenAmount = decimal.Parse(chosenAmountTextBox.Text.Trim());
            _budget = decimal.Parse(budgetTextBox.Text.Trim());
            _startingTime = startingDateTimePicker.Value;

            List<Runner> runners = _provider.GetRunnersForMarket(ProviderName, _matchId.ToString());
            _runnersIndexedById = new Dictionary<string, Runner>();

            // the loop does three things: 1. creates the dictionary 2. initialises alt1RunnerId and alt2RunnerId 3. composes match name
            _alt1RunnerId = _alt2RunnerId = string.Empty;
            _matchName = string.Empty;
            string alt1Name = string.Empty, alt2Name = string.Empty;
            foreach (Runner runner in runners)
            {
                _runnersIndexedById.Add(runner.Id, runner);
                if (runner.Id != _chosenRunnerId)
                {
                    if (string.IsNullOrEmpty(_alt1RunnerId))
                    {
                        _alt1RunnerId = runner.Id;
                        alt1Name = runner.Title;
                    }
                    else
                    {
                        _alt2RunnerId = runner.Id;
                        alt2Name = runner.Title;
                    }
                }
                _matchName += runner.Title + "-";
            }
            _matchName = _matchName.Substring(0, _matchName.Length - 1);
            Logger.Write(Resources.ProjectName, _matchName, "Initialising.");
            Logger.Write(Resources.ProjectName, _matchName, "Alt1: " + alt1Name + "; Alt2: " + alt2Name);
            Logger.Write(Resources.ProjectName, _matchName, "Chosen Odds: " + _chosenOdds);
        }

        private void getMarketsButton_Click(object sender, EventArgs e)
        {
            InitialiseBettingExchangeService();
            List<FootballMatch> markets = _provider.GetFootballMarkets(ProviderName);
            string marketId = string.Empty;
            markets.ForEach(m =>
            {
                if (m.Title.ToLower().Contains("burkina"))
                {
                    MessageBox.Show(m.Id.ToString());
                    marketId = m.Id.ToString();
                }
            });
            List<Runner> runners = _provider.GetRunnersForMarket(ProviderName, marketId);
        }

       

        private void startConsoleButton_Click(object sender, EventArgs e)
        {
            string marketId = matchIdTextBox.Text.Trim();
            string chosenRunnerId = chosenRunnerIdTextBox.Text.Trim();
            string chosenOdds = chosenOddsTextBox.Text.Trim();
            string chosenAmount = chosenAmountTextBox.Text.Trim();
            string budget = budgetTextBox.Text.Trim();
            DateTime startingTime = startingDateTimePicker.Value;
            string arguments = String.Format("{0} {1} {2} {3} {4} {5}", marketId, chosenRunnerId, chosenOdds, chosenAmount,
                budget, startingTime.ToString("yyyyMMddHHmmss"));
            string pathToConsoleApp = @"..\..\..\Build\Tamerlane.exe";

            ProcessStartInfo p = new ProcessStartInfo();
            p.Arguments = arguments;
                //"123 456 2.1 10 20 20130211150000";
            p.FileName = pathToConsoleApp;
            Process.Start(p);
        }

        private void manageRunnerAliasesButton_Click(object sender, EventArgs e)
        {
            RunnerAliasesForm form = new RunnerAliasesForm();
            form.Show();
        }
    }
}
