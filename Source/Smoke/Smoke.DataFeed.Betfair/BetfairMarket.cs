using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Provider.Betfair
{
    public class BetfairMarket
    {
        public int Id
        {
            get;
            set;
        }
        // use this to check if it is match odds market or some other.
        public string MarketName
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public DateTime EventDate
        {
            get;
            set;
        }

        public int RunnerCount
        {
            get;
            set;
        }
    }
}
