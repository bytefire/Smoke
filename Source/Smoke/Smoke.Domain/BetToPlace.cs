using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Contains data required to place a bet.
    /// </summary>
    [DataContract]
    public class BetToPlace
    {
        [DataMember]
        public int MarketId
        {
            get;
            set;
        }
        [DataMember]
        public decimal Price
        {
            get;
            set;
        }
        [DataMember]
        public int RunnerId
        {
            get;
            set;
        }
        [DataMember]
        public decimal Amount
        {
            get;
            set;
        }
    }
}
