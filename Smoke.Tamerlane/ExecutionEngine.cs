using CompanyName.ProductName.Domain;
using Smoke.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Tamerlane
{
    public class ExecutionEngine
    {
        public static bool PlaceBets(IBettingExchangeService provider, int marketId, int runner1Id, decimal amount1, decimal price1,
            int runner2Id, decimal amount2, decimal price2)
        {
            BetToPlace bet1 = new BetToPlace()
            {
                MarketId = marketId,
                RunnerId = runner1Id,
                Amount = amount1,
                Price = price1
            };

            BetToPlace bet2 = new BetToPlace()
            {
                MarketId = marketId,
                RunnerId = runner2Id,
                Amount = amount2,
                Price = price2
            };
            List<BetToPlace> bets = new List<BetToPlace>() { bet1, bet2 };
            // OkashTODO: For testing only! Doesnt place any bets...
            //return provider.PlaceBets("betfair", bets);
            return true;
        }
    }
}
