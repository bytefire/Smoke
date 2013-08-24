using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    /// <summary>
    /// Represents the bet that user wants to hedge against.
    /// </summary>
    public class BetToWatch
    {
        /// <summary>
        /// Unique id for every bet being watched.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Identifies market being watched.
        /// </summary>
        public int MarketId { get; set; }
        /// <summary>
        /// Identifies chosen runner.
        /// </summary>
        public int ChosenRunnerId { get; set; }
        /// <summary>
        /// The odds already bet upon.
        /// </summary>
        public decimal ChosenOdds { get; set; }
        /// <summary>
        /// The amount already bet.
        /// </summary>
        public decimal ChosenAmount { get; set; }
        /// <summary>
        /// The amount for optibet.
        /// </summary>
        public decimal OptibetAmount { get; set; }
        /// <summary>
        /// Identifies the client which requested the optibet.
        /// </summary>
        // OkashTODO: May be this should be device token instead. Because all we need to be able to
        //            respond to the request via push notifications is a device token.
        public string ClientId { get; set; }
    }
}
