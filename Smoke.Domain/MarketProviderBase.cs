using Smoke.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProductName.Domain
{
    public abstract class MarketProviderBase : IDisposable, IBettingExchangeService
    {
        // Track whether Dispose has been called.
        private bool _disposed = false;

        public abstract string Name { get; }

        public abstract List<EventType> GetAllEventTypes(string provider);

        public abstract List<FootballMatch> GetFootballMarkets(string provider);

        public abstract List<Market> GetCricketMarkets(string provider);

        public abstract bool PlaceBets(string provider, List<BetToPlace> betsToPlace);

        public abstract List<FootballMatch> GetFootballMarketsForCategory(string provider, string category);

        public abstract List<FootballMatch> GetFootballMarketsForLeague(string provider, string category, string league);

        public abstract List<Runner> GetRunnersForMarket(string provider, string category, string league, string marketId);

        public abstract List<Runner> GetRunnersForMarket(string provider, string marketId);

        public abstract Dictionary<int, ExchangePrice> GetOddsIndexedByRunnerId(int marketId);

        protected void EnsureCorrectProvider(string provider)
        {
            if (Name != provider)
                throw new InvalidOperationException("The specified provider name does not match the provider.");
        }

        public void Dispose()
        {

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

    }
}
