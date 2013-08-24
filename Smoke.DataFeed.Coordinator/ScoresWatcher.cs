using Smoke.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smoke.DataFeed.Coordinator
{
    public class ScoresWatcher : IObservable<FootballScore>
    {
        private const int RetryTimeout = 5 * 1000; // 10 sec, i.e. max 12 retries in a minute.
        // OkashTODO: Copy-pasted code for singleton. Need to understand what's going on. Read: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx
        #region Singleton Code
        // static holder for instance, need to use lambda to construct since constructor private
        private static readonly Lazy<ScoresWatcher> _instance
                 = new Lazy<ScoresWatcher>(() => new ScoresWatcher());
        // accessor for instance
        public static ScoresWatcher Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        #endregion
        private object _stopWatchingSyncRoot = new object();
        private bool _stopWatching = false;
        private ILiveScoresService _liveScoresApi = null;
        private List<IObserver<FootballScore>> _observers;
        private FootballScore _latestFootballScore = new FootballScore()
        {
            PublishedDateTime = DateTime.MinValue
        };

        private ScoresWatcher()
        {
            _observers = new List<IObserver<FootballScore>>();
            InitialiseLiveScoresService();
        }
        public IDisposable Subscribe(IObserver<FootballScore> observer)
        {
            _observers.Add(observer);
            return null;
        }
        public void UpdateSubscribers(FootballScore o)
        {
            _observers.ForEach(x => x.OnNext(o));
        }
        // runs a loop with sleep to check for price changes for the match with specified home and away team names.
        public void Start(string home, string away)
        {
            Task.Factory.StartNew(() => { StartAsync(home, away); }, TaskCreationOptions.LongRunning);
        }

        public void Stop()
        {
            lock (_stopWatchingSyncRoot)
            {
                _stopWatching = true;
            }
        }

        private void StartAsync(string home, string away)
        {
            while (true)
            {
                lock (_stopWatchingSyncRoot)
                {
                    if (_stopWatching)
                    {
                        break;
                    }
                }
                try
                {
                    FootballScore footballScore = _liveScoresApi.GetLatestFootballScore(home, away);
                    // update subsribers only if the update is more recent.
                    if (footballScore.PublishedDateTime > _latestFootballScore.PublishedDateTime)
                    {
                        _latestFootballScore = footballScore;
                        // OkashTODO: this happens in the same thread so the 10 sec wait below starts AFTER subscribers have processed
                        //              the update. this should happen in a separate thread.
                        UpdateSubscribers(_latestFootballScore);
                    }
                }
                catch
                {
                }
                Thread.Sleep(RetryTimeout);
            }
        }

        private void InitialiseLiveScoresService()
        {
            if (_liveScoresApi == null)
            {
                EndpointAddress liveScoresServiceAddress = new EndpointAddress("net.tcp://localhost:8586/LiveScoresService");
                _liveScoresApi = ChannelFactory<ILiveScoresService>.CreateChannel(new NetTcpBinding(), liveScoresServiceAddress);
            }
        }
    }
}
