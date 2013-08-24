using CompanyName.ProductName.Domain;
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
    public class PriceWatcher : IObservable<Dictionary<int, ExchangePrice>>
    {
        private const int RetryTimeout = 10 * 1000; // 10 sec, i.e. max 6 retries in a minute.
        // OkashTODO: Copy-pasted code for singleton. Need to understand what's going on. Read: http://geekswithblogs.net/BlackRabbitCoder/archive/2010/05/19/c-system.lazylttgt-and-the-singleton-design-pattern.aspx
        #region Singleton Code
        // static holder for instance, need to use lambda to construct since constructor private
        private static readonly Lazy<PriceWatcher> _instance
                 = new Lazy<PriceWatcher>(() => new PriceWatcher());
        // accessor for instance
        public static PriceWatcher Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        #endregion
        private object _stopWatchingSyncRoot = new object();
        private bool _stopWatching = false;
        private IBettingExchangeService _betfairProvider = null;
        private List<IObserver<Dictionary<int, ExchangePrice>>> _observers;
        private PriceWatcher()
        {
            _observers = new List<IObserver<Dictionary<int, ExchangePrice>>>();
            InitialiseBettingExchangeService();
        }
        public IDisposable Subscribe(IObserver<Dictionary<int, ExchangePrice>> observer)
        {
            _observers.Add(observer);
            return null;
        }
        public void UpdateSubscribers(Dictionary<int, ExchangePrice> o)
        {
            _observers.ForEach(x => x.OnNext(o));
        }
        // runs a loop with sleep to check for price changes for the specified market id.
        public void Start(int marketId)
        {
            Task.Factory.StartNew(() => { StartAsync(marketId); }, TaskCreationOptions.LongRunning);
        }

        public void Stop()
        {
            lock (_stopWatchingSyncRoot)
            {
                _stopWatching = true;
            }
        }

        private void StartAsync(int marketId)
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
                    Dictionary<int, ExchangePrice> runnersPrices = _betfairProvider.GetOddsIndexedByRunnerId(marketId);
                    // OkashTODO: this happens in the same thread so the 10 sec wait below starts AFTER subscribers have processed
                    //              the update. this should happen in a separate thread.
                    UpdateSubscribers(runnersPrices);
                }
                catch
                {
                }
                Thread.Sleep(RetryTimeout);
            }
        }

        private void InitialiseBettingExchangeService()
        {
            if (_betfairProvider == null)
            {
                EndpointAddress betfairServiceAddress = new EndpointAddress("net.tcp://localhost:8585/BetfairService");
                _betfairProvider = ChannelFactory<IBettingExchangeService>.CreateChannel(new NetTcpBinding(), betfairServiceAddress);
            }
        }
    }
}
