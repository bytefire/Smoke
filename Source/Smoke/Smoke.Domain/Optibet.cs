using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Represents optibet for a given BetToWatch instance.
    /// </summary>
    public class Optibet
    {
        /// <summary>
        /// Uniquely identifies the optibet.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Identifies the BetToWatch instance that this is an optbet for.
        /// </summary>
        public Guid BetToWatchId { get; set; }
        /// <summary>
        /// Date and time at which this instance of Optibet was calculated.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Array of optibet runners. Chosen runner will be at index zero.
        /// </summary>
        public OptibetRunner[] OptibetRunners { get; set; }
        /// <summary>
        /// The worst case scenario payoff.
        /// </summary>
        public decimal MinPayoff { get; set; }
    }
}
