using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Tamerlane
{
    public class Recommendation
    {
        public decimal RiskFreePayoff
        {
            get;
            set;
        }
        public decimal Alt1Amount
        {
            get;
            set;
        }
        public decimal Alt2Amount
        {
            get;
            set;
        }
        public decimal CounterBetAmount
        {
            get
            {
                return (Alt1Amount + Alt2Amount);
            }
        }
    }
}
