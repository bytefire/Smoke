using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    [DataContract]
    public class EventType
    {
        [DataMember]
        public int Id
        {
            get;
            set;
        }
        [DataMember]
        public string Name
        {
            get;
            set;
        }
    }
}
