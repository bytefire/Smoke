using Smoke.Domain;
using Smoke.Utilities.Aliases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Smoke.DataFeed.LiveScores
{
    public class ScoresProApi : IDisposable, ILiveScoresService
    {
        private const string FootballFeedSource = @"http://www.scorespro.com/rss/live-soccer.xml";
        // Track whether Dispose has been called.
        private bool _disposed = false;
        private XmlReader _xmlReader;
        SyndicationFeed _feed;
        // OkashTODO: Copy-pasted code for singleton. Need to understand what's going on. Read: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx
        #region Singleton Code
        // static holder for instance, need to use lambda to construct since constructor private
        private static readonly Lazy<ScoresProApi> _instance
                 = new Lazy<ScoresProApi>(() => new ScoresProApi());
        // accessor for instance
        public static ScoresProApi Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        #endregion

        public ScoresProApi()
        {
            _xmlReader = XmlReader.Create(FootballFeedSource);
        }

        public FootballScore GetLatestFootballScore(string team1Name, string team2Name)
        {
            FootballScore latestFootballScore = new FootballScore()
            {
                PublishedDateTime = DateTime.MinValue
            };
                //= new FootballScore();
            // 1. load all aliases for the two runners (team1 and team2). will need to add the
            //      method to do that in utilities project.
            NameAliases team1NameAliases = RunnerAliasesHelper.GetNameAliasesForRunner(team1Name);
            NameAliases team2NameAliases = RunnerAliasesHelper.GetNameAliasesForRunner(team2Name);
            // 2. get scorespro rss 
            _feed = SyndicationFeed.Load(_xmlReader);
            if (_feed == null)
            {
                return latestFootballScore;
            }
            foreach (SyndicationItem item in _feed.Items)
            {
                // 3. for each feed: 
                //      a. parse into football score object
                //      b. check if it contains both home and away alias
                //      c. if yes then cache it if latest. (same score could appear twice)
                Console.WriteLine(item.Title.Text);
                FootballScore parsedScore = Parser.ParseScoresProFootballScore(item);
                // set parsed score team names according to the caller of this method
                if (team1NameAliases.Aliases.Contains(parsedScore.HomeName) && team2NameAliases.Aliases.Contains(parsedScore.AwayName))
                {
                    parsedScore.HomeName = team1Name;
                    parsedScore.AwayName = team2Name;
                    if (parsedScore.PublishedDateTime > latestFootballScore.PublishedDateTime)
                    {
                        latestFootballScore = parsedScore;
                    }
                }
                if (team2NameAliases.Aliases.Contains(parsedScore.HomeName) && team1NameAliases.Aliases.Contains(parsedScore.AwayName))
                {
                    parsedScore.HomeName = team2Name;
                    parsedScore.AwayName = team1Name;
                    if (parsedScore.PublishedDateTime > latestFootballScore.PublishedDateTime)
                    {
                        latestFootballScore = parsedScore;
                    }
                }
            }
            // 4. return the latest score
            return latestFootballScore;
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the 
        // runtime from inside the finalizer and you should not reference 
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    CleanUpInternal();
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed.
                // unmanaged resources disposing goes here.

                // Note that this is not thread safe.
                // Another thread could start disposing the object
                // after the managed resources are disposed,
                // but before the disposed flag is set to true.
                // If thread safety is necessary, it must be
                // implemented by the client.

            }
            _disposed = true;
        }

        private void CleanUpInternal()
        {
        }
        #endregion
    }
}
