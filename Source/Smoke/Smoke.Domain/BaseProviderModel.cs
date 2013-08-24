using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// A base model common to all market provider data.
    /// </summary>
    [DataContract]
    public class BaseProviderModel
    {
        [DataMember]
        public string Id
        {
            get;
            set;
        }
        [DataMember]
        public string Provider
        {
            get;
            set;
        }
    }
}