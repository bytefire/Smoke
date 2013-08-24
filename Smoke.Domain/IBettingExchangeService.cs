using CompanyName.ProductName.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Domain
{
    [ServiceContract]
    public interface IBettingExchangeService
    {
        [OperationContract]
        List<EventType> GetAllEventTypes(string provider);
        [OperationContract]
        List<FootballMatch> GetFootballMarkets(string provider);
        [OperationContract]
        List<Market> GetCricketMarkets(string provider);
        [OperationContract]
        bool PlaceBets(string provider, List<BetToPlace> betsToPlace);
        [OperationContract]
        List<Runner> GetRunnersForMarket(string provider, string marketId);
        [OperationContract]
        Dictionary<int, ExchangePrice> GetOddsIndexedByRunnerId(int marketId);


    }
}
