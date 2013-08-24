using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Genghis
{
    public class DecisionEngine
    {
        private const int BreakEvenThreshold = 30;
        private const int RescueThreshold = 2;
        public static bool PlaceBet(decimal chosenAmount, decimal worstPayoff, DateTime expectedEndingTime)
        {
            int minutesLeft = (int)expectedEndingTime.Subtract(DateTime.Now).TotalMinutes;

            if (minutesLeft > BreakEvenThreshold)
            {
                if ((worstPayoff / chosenAmount) >= 0.1M)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if ((minutesLeft <= BreakEvenThreshold) && (minutesLeft > RescueThreshold))
            {
                if (worstPayoff >= 0.0M)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (minutesLeft <= RescueThreshold)
            {
                if (worstPayoff >= (-1 * chosenAmount))
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
    }
}
