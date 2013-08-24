using Smoke.Tamerlane.Properties;
using Smoke.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Tamerlane
{
    // performs calculations for Tamerlane
    public class ComputeEngine
    {
        public static Recommendation GetRecommendation(decimal chosenAmount, decimal chosenOdds, decimal alt1Odds, decimal alt2Odds,
            decimal budget, string matchName)
        {
            // 1. compute CBA
            decimal counterBetAmount = ComputeCounterBetAmount(chosenAmount, chosenOdds, alt1Odds, alt2Odds);
            // 2. compute RFP
            decimal riskFreePayoff = ComputeRiskFreePayoff(chosenAmount, chosenOdds, counterBetAmount);
            // 3. compute alt1 amount and alt2 amount
            // 4. if alt1 amount<2 or alt2 amount <2 then perform brute force optimisation
            decimal alt1Amount = ComputeAltAmount(counterBetAmount, alt1Odds, alt2Odds);
            if (alt1Amount < 2.0M)
            {
                Logger.Write(Resources.ProjectName, matchName, "Alt1 amount <2. So using brute force.");
                return GetRecommendationBruteForce(chosenAmount, chosenOdds, alt1Odds, alt2Odds, budget);
            }
            decimal alt2Amount = ComputeAltAmount(counterBetAmount, alt2Odds, alt1Odds);
            if (alt2Amount < 2.0M)
            {
                Logger.Write(Resources.ProjectName, matchName, "Alt2 amount <2. So using brute force.");
                return GetRecommendationBruteForce(chosenAmount, chosenOdds, alt1Odds, alt2Odds, budget);
            }
            Recommendation recommendation = new Recommendation()
            {
                RiskFreePayoff = riskFreePayoff,
                Alt1Amount = alt1Amount,
                Alt2Amount = alt2Amount
            };
            return recommendation;
        }
        public static decimal ComputeRiskFreePayoff(decimal chosenAmount, decimal chosenOdds, decimal alt1Odds, decimal alt2Odds)
        {
            // 1. compute counter bet amount
            decimal counterBetAmount = ComputeCounterBetAmount(chosenAmount, chosenOdds, alt1Odds, alt2Odds);
            // 2. compute risk free payoff
            decimal riskFreePayoff = ComputeRiskFreePayoff(chosenAmount, chosenOdds, counterBetAmount);
            return riskFreePayoff;
        }

        public static decimal ComputeCounterBetAmount(decimal chosenAmount, decimal chosenOdds, decimal alt1Odds, decimal alt2Odds)
        {
            decimal totalArbitragePercentage = (1 / alt1Odds) + (1 / alt2Odds);
            decimal counterBetAmount = totalArbitragePercentage * chosenAmount * chosenOdds;
            return counterBetAmount;
        }

        public static decimal ComputeRiskFreePayoff(decimal chosenAmount, decimal chosenOdds, decimal counterBetAmount)
        {
            decimal riskFreePayoff = chosenAmount * (chosenOdds - 1) - counterBetAmount;
            return riskFreePayoff;
        }

        public static decimal ComputeAltAmount(decimal counterBetAmount, decimal altToComputeOdds, decimal otherAltOdds)
        {
            decimal totalArbitragePercentage = (1 / altToComputeOdds) + (1 / otherAltOdds);
            decimal altToComputeArbitragePercentage = 1 / altToComputeOdds;
            decimal altToComputeAmount = counterBetAmount * altToComputeArbitragePercentage / totalArbitragePercentage;
            return altToComputeAmount;
        }
        public static Recommendation GetRecommendationBruteForce(decimal chosenAmount, decimal chosenOdds, decimal alt1Odds, decimal alt2Odds, decimal budget)
        {
            decimal chosenWinPayoff, alt1WinPayoff, alt2WinPayoff, minPayoff;
            Recommendation recommendation = new Recommendation();
            recommendation.RiskFreePayoff = decimal.MinValue;
            for (decimal localMax = 4.0M; localMax < budget; localMax += 0.1M)
            {
                for (decimal alt1Share = 2.0M, alt2Share = localMax - alt1Share; (alt1Share <= localMax) && (alt2Share >= 2.0M); alt1Share += 0.01M)
                {
                    chosenWinPayoff = GetPayoff(chosenOdds, chosenAmount, alt1Share, alt2Share);
                    alt1WinPayoff = GetPayoff(alt1Odds, alt1Share, chosenAmount, alt2Share);
                    alt2WinPayoff = GetPayoff(alt2Odds, alt2Share, chosenAmount, alt1Share);
                    minPayoff = Math.Min(Math.Min(chosenWinPayoff, alt1WinPayoff), alt2WinPayoff);
                    if (minPayoff > recommendation.RiskFreePayoff)
                    {
                        recommendation.RiskFreePayoff = minPayoff;
                        recommendation.Alt1Amount = alt1Share;
                        recommendation.Alt2Amount = alt2Share;
                    }
                    alt2Share = localMax - alt1Share;
                }
            }
            return recommendation;
        }

        private static decimal GetPayoff(decimal winnerOdds, decimal winnerAmount, decimal loser1Amount, decimal loser2Amount)
        {
            return ((winnerOdds - 1) * winnerAmount) - loser1Amount - loser2Amount;
        }
    }
}
