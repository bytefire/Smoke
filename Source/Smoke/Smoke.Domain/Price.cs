using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Models a price (odds) from a given provider. 
    /// </summary>
    [DataContract]
    public class Price
    {
        /// <summary>
        /// Gets and sets the odds.
        /// </summary>
        [DataMember]
        public decimal Odds
        {
            get;
            set;
        }

        public Price(decimal odds)
        {
            Odds = odds;
        }
    }
}
