using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Models a runner (bet) a user can place for a given market
    /// </summary>
    [DataContract]
    public class Runner : BaseProviderModel
    {
        /// <summary>
        /// The title of the runner.
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
        public Runner() : base()

        {
        }
    }
}