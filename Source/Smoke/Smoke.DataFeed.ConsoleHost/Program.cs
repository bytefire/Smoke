using CompanyName.ProductName.Provider.Betfair;
using Smoke.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.DataFeed.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // For WCF Hosting details see: http://msdn.microsoft.com/en-us/library/ee939340.aspx
            // This article also describes how to define endpoints in config file.
            ServiceHost betfairHost = null;
            ServiceHost liveScoresHost = null;
            try
            {
                betfairHost = new ServiceHost(typeof(BetfairMarketProvider));
                ServiceEndpoint betfairEndpoint = betfairHost.AddServiceEndpoint(typeof(IBettingExchangeService),
                    new NetTcpBinding(), "net.tcp://localhost:8585/BetfairService");

                betfairHost.Faulted += new EventHandler(Host_Faulted);
                betfairHost.Open();
                Console.WriteLine("Betfair service host started.");

                liveScoresHost = new ServiceHost(typeof(BetfairMarketProvider));
                ServiceEndpoint liveScoresEndpoint = betfairHost.AddServiceEndpoint(typeof(IBettingExchangeService),
                    new NetTcpBinding(), "net.tcp://localhost:8585/LiveScoresService");

                liveScoresHost.Faulted += new EventHandler(Host_Faulted);
                liveScoresHost.Open();
                Console.WriteLine("Live scores service host started.");


                Console.WriteLine("Press <enter> to terminate  the Application");
                Console.ReadKey(true);

            }
            finally
            {
                if (betfairHost.State == CommunicationState.Faulted)
                {
                    betfairHost.Abort();
                }
                else
                {
                    betfairHost.Close();
                }

                if (liveScoresHost.State == CommunicationState.Faulted)
                {
                    liveScoresHost.Abort();
                }
                else
                {
                    liveScoresHost.Close();
                }
            }
        }

        private static void Host_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
