using Smoke.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.DataFeed.LiveScores
{
    /// <summary>
    /// Contains helper methods to parse football score data.
    /// </summary>
    public class Parser
    {
        public static FootballScore ParseScoresProFootballScore(SyndicationItem itemToParse)
        {
            FootballScore parsedFootballScore = new FootballScore();
            // example score text= Soccer Livescore: (BRA-CA) #Madureira vs #Bangu: 1-1
            string scoreText = itemToParse.Title.Text;
            string[] splitOnColon = scoreText.Split(':');
            string[] splitOnHash = splitOnColon[1].Split('#');
            parsedFootballScore.HomeName = splitOnHash[1].Substring(0, splitOnHash[1].IndexOf(" vs "));
            parsedFootballScore.AwayName = splitOnHash[2];
            string[] splitOnHyphen = splitOnColon[2].Split('-');
            parsedFootballScore.HomeScore = int.Parse(splitOnHyphen[0].Trim());
            parsedFootballScore.AwayScore = int.Parse(splitOnHyphen[1].Trim());
            // parsing finished. extract other items.
            parsedFootballScore.Description = itemToParse.Summary.Text;
            parsedFootballScore.PublishedDateTime = itemToParse.PublishDate.DateTime;
            return parsedFootballScore;
        }
    }
}
