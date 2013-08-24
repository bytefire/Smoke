using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Domain
{
    [DataContract]
    public class FootballScore
    {
        // used to convert to string like Chelsea vs Real Madrid: 2-3 - Half Time | Published: 12 Mar 2013 20:25
        private const string FootballScoreStringFormat = "{0} vs {1}: {2}-{3} - {4} | Published: {5}";
        [DataMember]
        public string HomeName
        {
            get;
            set;
        }
        [DataMember]
        public string AwayName
        {
            get;
            set;
        }
        [DataMember]
        public int HomeScore
        {
            get;
            set;
        }
        [DataMember]
        public int AwayScore
        {
            get;
            set;
        }
        [DataMember]
        public DateTime PublishedDateTime
        {
            get;
            set;
        }
        [DataMember]
        public string Description
        {
            get;
            set;
        }

        public override string ToString()
        {
            return String.Format(FootballScoreStringFormat, HomeName, AwayName, HomeScore, 
                AwayScore, Description, PublishedDateTime.ToString("r"));
        }
    }
}
