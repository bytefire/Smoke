using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyName.ProductName.Provider.Betfair
{
    internal class BetfairMarketInfo
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public bool IsActive 
        { 
            get; 
            set; 
        }

        public List<BetfairRunner> Runners
        {
            get;
            set;
        }

        public BetfairMarketInfo()
        {
            Runners = new List<BetfairRunner>();
        }
    }
}
