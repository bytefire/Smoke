using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Same as <c>Runner</c> with <c>Price</c> added in.
    /// </summary>
    public class RunnerWithPrice : Runner
    {
        /// <summary>
        /// Gets and sets the price info.
        /// </summary>
        public Price Price
        {
            get;
            set;
        }

        public RunnerWithPrice()
            : base()
        {
        }
    }
}
