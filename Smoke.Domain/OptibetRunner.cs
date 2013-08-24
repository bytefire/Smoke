using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Represents a runner with recommended optibet properties.
    /// </summary>
    public class OptibetRunner
    {
        /// <summary>
        /// Identifies the runner.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Optibet (or chosen) amount.
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The payoff if this runner wins.
        /// </summary>
        public decimal PayoffIfWins { get; set; }
        /// <summary>
        /// Indicates if this is the chosen bet. 
        /// It will be false for the recommended optibets.
        /// </summary>
        public bool IsChosen { get; set; }
    }
}
