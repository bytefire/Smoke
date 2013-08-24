using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyName.ProductName.Provider.Betfair.WebServices.ExchangeService;

namespace CompanyName.ProductName.Provider.Betfair
{
    internal class MarketInfoParser
    {
        internal BetfairMarketInfo ParseMarketInfo(Market marketData)
        {
            BetfairMarketInfo marketInfo = new BetfairMarketInfo();
            marketInfo.Id = marketData.marketId;
            marketInfo.Name = marketData.name;
            marketInfo.Description = marketData.marketDescription;
            marketInfo.IsActive = marketData.marketStatus == MarketStatusEnum.ACTIVE ? true : false;
            
            foreach(Runner runnerData in marketData.runners)
            {
                BetfairRunner runner = ParseRunner(runnerData);
                marketInfo.Runners.Add(runner);
            }

            return marketInfo;
        }

        private BetfairRunner ParseRunner(Runner runnerData)
        {
            BetfairRunner runner = new BetfairRunner();
            runner.Id = runnerData.selectionId;
            runner.Name = runnerData.name;
            return runner;
        }
    }
}
