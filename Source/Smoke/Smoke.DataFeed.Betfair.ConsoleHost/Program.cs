using CompanyName.ProductName.Provider.Betfair;
using Smoke.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.DataFeed.Betfair.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // For WCF Hosting details see: http://msdn.microsoft.com/en-us/library/ee939340.aspx
            // This article also describes how to define endpoints in config file.
            ServiceHost host = null;
            try
            {
                host = new ServiceHost(typeof(BetfairMarketProvider));
                ServiceEndpoint endpoint = host.AddServiceEndpoint(typeof(IBettingExchangeService),
                    new NetTcpBinding(), "net.tcp://localhost:8585/BetfairService");

                host.Faulted += new EventHandler(Host_Faulted);
                host.Open();
                Console.WriteLine("Press <enter> to terminate  the Application");
                Console.ReadKey(true);

            }
            finally
            {
                if (host.State == CommunicationState.Faulted)
                {
                    host.Abort();
                }
                else
                {
                    host.Close();
                }
            }
        }

        private static void Host_Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
