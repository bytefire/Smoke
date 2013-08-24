using CompanyName.ProductName.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Genghis
{
    public class Optimiser
    {
        public static OptimumValues Optimise(Dictionary<int, ExchangePrice> oddsIndexedByRunnerId, int chosenRunnerId,
            decimal chosenOdds, decimal chosenAmount, decimal budget)
        {
            int[] altRunners = oddsIndexedByRunnerId.Where(p => p.Key != chosenRunnerId).Select(p => p.Key).ToArray();

            OptimumValues optimumValues = new OptimumValues();
            optimumValues.ChosenAlternativeId = chosenRunnerId;
            optimumValues.ChosenAlternativeOdds = chosenOdds;
            optimumValues.ChosenAlternativeAmount = chosenAmount;

            optimumValues.Alternative1Id = altRunners[0];
            optimumValues.Alternative1Odds = oddsIndexedByRunnerId[altRunners[0]].Odds;
            optimumValues.Alternative2Id = altRunners[1];
            optimumValues.Alternative2Odds = oddsIndexedByRunnerId[altRunners[1]].Odds;


            decimal chosenWinPayoff, alt1WinPayoff, alt2WinPayoff, minPayoff;
            
            for (decimal localMax = 4.0M; localMax < budget; localMax += 0.1M)
            {
                for (decimal alt1Share = 2.0M, alt2Share = localMax - alt1Share; (alt1Share <= localMax) && (alt2Share>=2.0M); alt1Share += 0.01M)
                {
                    chosenWinPayoff = GetPayoff(chosenOdds, chosenAmount, alt1Share, alt2Share);
                    alt1WinPayoff = GetPayoff(oddsIndexedByRunnerId[altRunners[0]].Odds, alt1Share, chosenAmount, alt2Share);
                    alt2WinPayoff = GetPayoff(oddsIndexedByRunnerId[altRunners[1]].Odds, alt2Share, chosenAmount, alt1Share);
                    minPayoff = Math.Min(Math.Min(chosenWinPayoff, alt1WinPayoff), alt2WinPayoff);
                    if (minPayoff > optimumValues.WorstPayoff )
                    {
                        optimumValues.WorstPayoff = minPayoff;
                        optimumValues.Alternative1Amount = alt1Share;
                        optimumValues.Alternative2Amount = alt2Share;
                        optimumValues.PayoffChosenAlternativeWin = chosenWinPayoff;
                        optimumValues.PayoffAlternative1Win = alt1WinPayoff;
                        optimumValues.PayoffAlternative2Win = alt2WinPayoff;
                    }
                    alt2Share = localMax - alt1Share;
                }
            }

            return optimumValues;
        }

        private static decimal GetPayoff(decimal winnerOdds, decimal winnerAmount, decimal loser1Amount, decimal loser2Amount)
        {
            return ((winnerOdds - 1) * winnerAmount) - loser1Amount - loser2Amount;
        }
    }
}
