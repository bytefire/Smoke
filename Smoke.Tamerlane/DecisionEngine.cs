using Smoke.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Tamerlane
{
    public class DecisionEngine
    {
        // a threshold fraction represents fraction of chosen amount that we expect as the net payoff of 
        // all the bets (i.e. one chosen bet and two counter bets)
        private const decimal NormalThresholdFraction = 0.3M;
        private const decimal RescueThresholdFraction = -1.0M;

        public static bool PlaceBet2(decimal chosenAmount, decimal riskFreePayoff, DateTime startingTime,
            int chosenScore, int opponentScore)
        {
            decimal rescueThresholdAmount = GetThresholdAmount(chosenAmount, (int)DateTime.Now.Subtract( startingTime).TotalMinutes,
                chosenScore, opponentScore);
            return (riskFreePayoff >= rescueThresholdAmount);
           
        }

        private static decimal GetThresholdAmount(decimal chosenAmount, int minutesSinceStart, int chosenScore, int opponentScore)
        {
            // if timeSinceStart> 40 and match is a draw then return rescue threshold amount
            if ((minutesSinceStart > 40) && (chosenScore == opponentScore))
            {
                return RescueThresholdFraction * chosenAmount;
            }
            // if timeSinceStart < 20 and opponent ahead then return breakeven threshold
            if ((minutesSinceStart < 20) && (chosenScore < opponentScore))
            {
                return 0; // breakeven
            }
            // if timeSinceStart > 20 and opponent ahead then return rescue threshold
            if ((minutesSinceStart > 20) && (chosenScore < opponentScore))
            {
                return RescueThresholdFraction * chosenAmount;
            }
            // otherwise return normal threshold amount
            return NormalThresholdFraction * chosenAmount;
        }

        #region Old Code
        private const int BreakEvenThreshold = 30;
        private const int RescueThreshold = 88;
        public static bool PlaceBet(decimal chosenAmount, decimal riskFreePayoff, DateTime startingTime, FootballScore score)
        {
            int minutesSinceStart = (int)DateTime.Now.Subtract(startingTime).TotalMinutes;

            if (minutesSinceStart < BreakEvenThreshold)
            {
                if ((riskFreePayoff / chosenAmount) >= 0.3M)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if ((minutesSinceStart >= BreakEvenThreshold) && (minutesSinceStart < RescueThreshold))
            {
                if (riskFreePayoff >= 0.0M)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (minutesSinceStart >= RescueThreshold)
            {
                if (riskFreePayoff >= (-1 * chosenAmount))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}
