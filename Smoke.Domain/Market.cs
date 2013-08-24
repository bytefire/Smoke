using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Models a market a user can place a bet on.
    /// </summary>
    [DataContract]
    public class Market : BaseProviderModel
    {
        /// <summary>
        /// Gets and sets the title of the market.
        /// </summary>
        [DataMember]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Market() : base()
        {

        }
    }
}