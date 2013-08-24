using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Genghis
{
    public class OptimumValues
    {
        private const int MaxLengthOfRunnerName = 15;
        private const string NotificationStringFormat = "{0}*{1}*{2}*{3}*{4}*{5}*{6}*{7}*{8}*{9}";
        public int ChosenAlternativeId
        {
            get;
            set;
        }
        public string ChosenAlternativeName
        {
            get;
            set;
        }
        public decimal ChosenAlternativeAmount
        {
            get;
            set;
        }
        public decimal ChosenAlternativeOdds
        {
            get;
            set;
        }

        public int Alternative1Id
        {
            get;
            set;
        }
        public string Alternative1Name
        {
            get;
            set;
        }
        public decimal Alternative1Amount
        {
            get;
            set;
        }
        public decimal Alternative1Odds
        {
            get;
            set;
        }
        public int Alternative2Id
        {
            get;
            set;
        }
        public string Alternative2Name
        {
            get;
            set;
        }
        public decimal Alternative2Amount
        {
            get;
            set;
        }
        public decimal Alternative2Odds
        {
            get;
            set;
        }
        public decimal PayoffChosenAlternativeWin
        {
            get;
            set;
        }
        public decimal PayoffAlternative1Win
        {
            get;
            set;
        }
        public decimal PayoffAlternative2Win
        {
            get;
            set;
        }
        public decimal WorstPayoff
        {
            get;
            set;
        }

        private string ChosenAlternativeNameTrimmed
        {
            get
            {
                return ChosenAlternativeName.Length <= MaxLengthOfRunnerName ? ChosenAlternativeName :
                ChosenAlternativeName.Substring(0, MaxLengthOfRunnerName);
            }
        }

        private string Alternative1NameTrimmed
        {
            get
            {
                return Alternative1Name.Length <= MaxLengthOfRunnerName ? Alternative1Name :
                Alternative1Name.Substring(0, MaxLengthOfRunnerName);
            }
        }

        private string Alternative2NameTrimmed
        {
            get
            {
                return Alternative2Name.Length <= MaxLengthOfRunnerName ? Alternative2Name :
                Alternative2Name.Substring(0, MaxLengthOfRunnerName);
            }
        }

        public OptimumValues()
        {
            WorstPayoff = Decimal.MinValue;
        }
        /// <summary>
        /// Composes a asterisk separated string of values to be passed as notification.
        /// </summary>
        /// <returns></returns>
        public string GetNotificationString()
        {
            return String.Format(NotificationStringFormat, ChosenAlternativeNameTrimmed, Alternative1NameTrimmed, Alternative1Amount,
                Alternative1Odds, Alternative2NameTrimmed, Alternative2Amount, Alternative2Odds,
                PayoffChosenAlternativeWin, PayoffAlternative1Win, PayoffAlternative2Win);
        }
        public string GetMatchTitle()
        {
            return String.Format("{0}-{1}-{2}", ChosenAlternativeNameTrimmed, Alternative1NameTrimmed, Alternative2NameTrimmed);
        }
    }
}
