using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Models a price provided by a bookmaker which is an exchange.
    /// </summary>
    [DataContract]
    public class ExchangePrice : Price
    {
        /// <summary>
        /// Gets and sets the un-matched amount.
        /// </summary>
        [DataMember]
        public decimal UnMatchedAmount
        {
            get;
            set;
        }
        // the default value of unamtched amount is invalid. unmatched amount cannot be less than zero.
        public ExchangePrice( decimal odds, decimal unMatchedAmount = -1) : base(odds)
        {
            UnMatchedAmount = unMatchedAmount;
        }
    }
}
