using CompanyName.ProductName.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExchangeService = CompanyName.ProductName.Provider.Betfair.WebServices.ExchangeService;
using GlobalService = CompanyName.ProductName.Provider.Betfair.WebServices.GlobalService;

namespace CompanyName.ProductName.Provider.Betfair
{
    internal class BetfairApi : IDisposable
    {
        #region Singleton Code
        private static volatile BetfairApi instance;
   private static object syncRoot = new Object();



   public static BetfairApi Instance
   {
      get 
      {
         if (instance == null) 
         {
            lock (syncRoot) 
            {
               if (instance == null)
                   instance = new BetfairApi();
            }
         }

         return instance;
      }
   }
        #endregion

        // Track whether Dispose has been called.
        private bool _disposed = false;

        // Betfair login credentials
        private const string Username = "ENTER_BETFAIR_USERNAME_HERE";
        private const string Password = "ENTER_BETFAIR_PASSWORD_HERE";
        private const int ProductId = 82;
        private GlobalService.BFGlobalServiceClient _globalService;
        private ExchangeService.BFExchangeServiceClient _exchangeService;

        private const string LocaleEnglish = "en";
        private const int FootballEventTypeId = 1;
        private const int TennisEventTypeId = 2;
        private const int GolfEventTypeId = 3;
        private const int CricketEventTypeId = 4;
        private const int RugbyUnionEventTypeId = 5;
        private const int BoxingEventTypeId = 6;
        private const int HorseRacingEventTypeId = 7;

        // Interval after connection manager will perform a KeepAlive call 
        // to refresh the session.
        private const int SessionRefreshIntervalInSeconds = 20 * 60; // 20 mins
        // Timeout handle to wait for SessionRefreshIntervalInSeconds before refreshing
        // session token. This should never be set.
        private static ManualResetEvent _timeoutHandle = new ManualResetEvent(false);

        private string _sessionToken = string.Empty;
        private DateTime _sessionTokenLastUpdated = DateTime.MinValue;

        private BetfairApi()
        {
            BasicHttpBinding httpBinding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
            httpBinding.MaxReceivedMessageSize = 10000000;

            _globalService = new GlobalService.BFGlobalServiceClient(httpBinding, new EndpointAddress("https://api.betfair.com/global/v3/BFGlobalService"));
            _exchangeService = new ExchangeService.BFExchangeServiceClient(httpBinding, new EndpointAddress("https://api.betfair.com/exchange/v5/BFExchangeService"));

            LogInToService(Username, Password, ProductId);

            Task maintainConnectionTask = Task.Factory.StartNew(MaintainConnection, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Runs an infinite loop which updates login session every SessionRefreshIntervalInSeconds.
        /// </summary>
        private void MaintainConnection()
        {
            while (true)
            {
                _timeoutHandle.WaitOne(SessionRefreshIntervalInSeconds * 1000);

                if (String.IsNullOrEmpty(_sessionToken))
                {
                    // Log in to obtain the session token.
                    LogInToService(Username, Password, ProductId);
                }
                else if (DateTime.Now.Subtract(_sessionTokenLastUpdated).TotalSeconds > SessionRefreshIntervalInSeconds)
                {
                    // Keep Alive
                    KeepAlive(_sessionToken);
                }

            }
        }
        private void KeepAlive(string sessionToken)
        {
            GlobalService.KeepAliveReq keepAliveRequest = new GlobalService.KeepAliveReq();
            keepAliveRequest.header = new GlobalService.APIRequestHeader();
            keepAliveRequest.header.sessionToken = sessionToken;
            GlobalService.KeepAliveResp keepAliveResponse = _globalService.keepAlive(keepAliveRequest);
            if (keepAliveResponse == null || keepAliveResponse.header.errorCode != GlobalService.APIErrorEnum.OK)
            {
                // OkashTODO: put this as a trace statement and throw exception saying unable 
                // to refresh session token.
                return;
            }

            MaintainSession(keepAliveResponse.header.sessionToken);
        }

        internal List<EventType> GetAllEventTypes()
        {
            List<EventType> eventTypes = GetAllEventTypesFromService();
            return eventTypes;
        }

        internal List<BetfairMarket> GetFootballMarkets()
        {
            int?[] marketTypeIds = new int?[] { FootballEventTypeId };
            List<BetfairMarket> markets = GetMarketsFromService(marketTypeIds);
            return markets;
        }

        internal List<BetfairMarket> GetCricketMarkets()
        {
            int?[] marketTypeIds = new int?[] { CricketEventTypeId };
            List<BetfairMarket> markets = GetMarketsFromService(marketTypeIds);
            return markets;
        }

        /*
        internal List<BetfairMarket> GetCricketMarkets()
        {
            int?[] marketIds = new int?[] { FootballEventTypeId };
            List<BetfairMarket> markets = GetMarketsFromService(marketIds);
            return markets;
        }
         * */

        internal bool PlaceBets(List<BetToPlace> betsToPlace)
        {
            // OkashTODO: Extract all that narrows down the request to our specific situation
            //            out of the PlaceBetsWithService method and into this method.
            return PlaceBetsWithService(betsToPlace);
        }

        internal BetfairMarketInfo GetMarketInfo(int marketId)
        {
            return GetMarketInfoFromService(marketId);
        }

        internal Dictionary<int, ExchangePrice> GetOddsIndexByRunner(int marketId)
        {
            Dictionary<int, ExchangePrice> oddsIndexByRunner = GetOddsIndexByRunnerFromService(marketId);
            return oddsIndexByRunner;
        }

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
            LogOutOfService();

            // TODO: Lookup best practices for closing WCF connections.
            _globalService.Close();
            _globalService = null;
        }

        private Dictionary<int, ExchangePrice> GetOddsIndexByRunnerFromService(int marketId)
        {
            ExchangeService.GetCompleteMarketPricesCompressedReq request = new ExchangeService.GetCompleteMarketPricesCompressedReq();
            request.marketId = marketId;
            request.header = CreateExchangeRequestHeader(_sessionToken);

            ExchangeService.GetCompleteMarketPricesCompressedResp response = _exchangeService.getCompleteMarketPricesCompressed(request);

            MaintainSession(response.header.sessionToken);

            RunnerOddsInfoDataParser parser = new RunnerOddsInfoDataParser();
            Dictionary<int, ExchangePrice> oddInfoIndexByRunner = parser.ParseOddsInfo(response.completeMarketPrices);
            return oddInfoIndexByRunner;
        }

        private List<EventType> GetAllEventTypesFromService()
        {
            GlobalService.GetEventTypesReq request = new GlobalService.GetEventTypesReq();
            request.header = CreateGlobalRequestHeader(_sessionToken);
            request.locale = LocaleEnglish;

            GlobalService.GetEventTypesResp response = _globalService.getAllEventTypes(request);

            MaintainSession(response.header.sessionToken);

            List<EventType> eventTypes = new List<EventType>();
            foreach (GlobalService.EventType eventTypeItem in response.eventTypeItems)
            {
                eventTypes.Add(new EventType()
                {
                    Id = eventTypeItem.id,
                    Name = eventTypeItem.name
                });
                
            }
            return eventTypes;
        }

        private List<BetfairMarket> GetMarketsFromService(int?[] marketIds)
        {
            ExchangeService.GetAllMarketsReq request = new ExchangeService.GetAllMarketsReq();
            request.header = CreateExchangeRequestHeader(_sessionToken);
            request.eventTypeIds = marketIds;
            request.locale = LocaleEnglish;
            request.fromDate = DateTime.Today;
            request.toDate = DateTime.Today.AddDays(1);
            //request.countries = new string[] { "GBR" };

            ExchangeService.GetAllMarketsResp response = _exchangeService.getAllMarkets(request);

            MaintainSession(response.header.sessionToken);


            MarketDataParser parser = new MarketDataParser();
            List<BetfairMarket> markets = parser.ParseMatchOddsMarkets(response.marketData);
            return markets;
        }

        // OkashTODO: Extract all that narrows down the request to our specific situation
        //            out of the this method and into PlaceBets (the caller of this method).
        private bool PlaceBetsWithService(List<BetToPlace> betsToPlace)
        {
            ExchangeService.PlaceBetsReq request = new ExchangeService.PlaceBetsReq();
            request.header = CreateExchangeRequestHeader(_sessionToken);
            //request.bets 
            ExchangeService.PlaceBets[] placeBets = new ExchangeService.PlaceBets[betsToPlace.Count];
            ExchangeService.PlaceBets bet;
            int counter = 0;
            foreach (BetToPlace betToPlace in betsToPlace)
            {
                bet = new ExchangeService.PlaceBets();
                bet.asianLineId = 0;
                bet.betCategoryType = ExchangeService.BetCategoryTypeEnum.E;
                bet.betPersistenceType = ExchangeService.BetPersistenceTypeEnum.IP;
                bet.betType = ExchangeService.BetTypeEnum.B;
                // OkashTODO: find out what bsp liability actually means for in-play exchange bets. 
                bet.bspLiability = (double)betToPlace.Amount;
                bet.marketId = betToPlace.MarketId;
                bet.price = (double)betToPlace.Price;
                bet.selectionId = betToPlace.RunnerId;
                bet.size = (double)betToPlace.Amount;
                placeBets[counter] = bet;
                counter++;
            }
            request.bets = placeBets;
            ExchangeService.PlaceBetsResp response = _exchangeService.placeBets(request);

            MaintainSession(response.header.sessionToken);

            foreach (ExchangeService.PlaceBetsResult result in response.betResults)
            {
                if (!result.success)
                {
                    return false;
                }
            }
            return true;
        }

        private BetfairMarketInfo GetMarketInfoFromService(int marketId)
        {
            ExchangeService.GetMarketReq request = new ExchangeService.GetMarketReq();
            request.marketId = marketId;
            request.header = CreateExchangeRequestHeader(_sessionToken);

            ExchangeService.GetMarketResp response = _exchangeService.getMarket(request);

            MaintainSession(response.header.sessionToken);

            MarketInfoParser parser = new MarketInfoParser();
            BetfairMarketInfo marketInfo = parser.ParseMarketInfo(response.market);
            return marketInfo;
        }

        private void LogOutOfService()
        {
            // 
            if (string.IsNullOrWhiteSpace(_sessionToken))
            {
                return;
            }

            GlobalService.APIRequestHeader requestHeader = CreateGlobalRequestHeader(_sessionToken);

            GlobalService.LogoutReq request = new GlobalService.LogoutReq();
            request.header = requestHeader;
            GlobalService.LogoutResp response = _globalService.logout(request);

            // TODO: Error handling.

            // Track the session token.
            // Even when logging out?? That's like drawing some petty cash from ATM before dying.
            // MaintainGlobalSession(response.header);
        }


        private void LogInToService(string username, string password, int productId)
        {
            GlobalService.LoginReq request = new GlobalService.LoginReq();
            request.username = username;
            request.password = password;
            request.productId = productId;
            GlobalService.LoginResp response = _globalService.login(request);
            Debug.Assert(response != null, "LoginResp == null");
            Debug.Assert(response.header != null, "LoginResp.Header == null");
            // Track the session token.
            MaintainSession(response.header.sessionToken);
            // TODO: Error handling.
        }

        private void ExecuteWcfMethod(Action action)
        {
            // TODO. Add error handling here.

            action();
        }

        private GlobalService.APIRequestHeader CreateGlobalRequestHeader(string sessionToken)
        {
            // ResourceTODO: SessionTokenNotInitialized
            if (sessionToken == null)
                throw new ArgumentNullException("sessionToken", "The betfair session token has not been initialised.");

            GlobalService.APIRequestHeader requestHeader = new GlobalService.APIRequestHeader();
            requestHeader.sessionToken = sessionToken;
            return requestHeader;
        }

        private ExchangeService.APIRequestHeader CreateExchangeRequestHeader(string sessionToken)
        {
            // ResourceTODO: SessionTokenNotInitialized
            if (sessionToken == null)
                throw new ArgumentNullException("sessionToken", "The betfair session token has not been initialised.");

            ExchangeService.APIRequestHeader requestHeader = new ExchangeService.APIRequestHeader();
            requestHeader.sessionToken = sessionToken;
            return requestHeader;
        }


        private void MaintainSession(string sessionToken)
        {
            Debug.Assert(!string.IsNullOrEmpty(sessionToken), "string.IsNullOrEmpty(sessionToken)");
            _sessionToken = sessionToken;
            _sessionTokenLastUpdated = DateTime.Now;
        }

    }
}
