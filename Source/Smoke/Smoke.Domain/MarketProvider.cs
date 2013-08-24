using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Models a bookmaker.
    /// </summary>
    public class MarketProvider : BaseModel
    {
        /// <summary>
        /// Gets or sets the name of the bookmaker.
        /// </summary>
        public string Name
        {
            get;
            set;
        }
    }
}
