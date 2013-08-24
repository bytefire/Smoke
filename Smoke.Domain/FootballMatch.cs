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
    public class FootballMatch : Market
    {
        /// <summary>
        /// Gets or sets the name of the home team.
        /// </summary>
        [DataMember]
        public string HomeTeam
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the away team.
        /// </summary>
        [DataMember]
        public string AwayTeam
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of game, English, Spanish, European etc...
        /// </summary>
        [DataMember]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the league name i.e Premiership, La Liga, Champions League, European Championship etc...
        /// </summary>
        [DataMember]
        public string League
        {
            get;
            set;
        }



        /// <summary>
        /// 
        /// </summary>
        public FootballMatch() : base()
        {

        }
    }
}