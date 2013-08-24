using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Utilities;

namespace CompanyName.ProductName.Provider.Betfair
{
    internal class MarketDataParser : Parser
    {
        private const char MarketDataSplitCharacter = ':';
        private const char MarketFieldSplitCharacter = '~';
        private const string MatchOddsMarketName = "Match Odds";

        internal List<BetfairMarket> ParseMarkets(string marketsData)
        {
            List<BetfairMarket> parsedMarkets = new List<BetfairMarket>();

            // Replace any escaped characters with the placeholder value so that 
            // we dont split on the escaped character. A field may contain the character
            // ':' in its value.
            string replacedString = ReplaceEscapedCharacters(marketsData, MarketDataSplitCharacter);
            
            // We can get the list of markets by splitting the data on the split character.
            string[] markets = replacedString.Split(new[] { MarketDataSplitCharacter }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string market in markets)
            {
                BetfairMarket parsedMarket = ParseMarket(market);
                if (parsedMarket != null)
                {
                    parsedMarkets.Add(parsedMarket);
                }
            }

            return parsedMarkets;
        }

        internal List<BetfairMarket> ParseMatchOddsMarkets(string marketsData)
        {
            List<BetfairMarket> parsedMarkets = new List<BetfairMarket>();

            // Replace any escaped characters with the placeholder value so that 
            // we dont split on the escaped character. A field may contain the character
            // ':' in its value.
            string replacedString = ReplaceEscapedCharacters(marketsData, MarketDataSplitCharacter);

            // We can get the list of markets by splitting the data on the split character.
            string[] markets = replacedString.Split(new[] { MarketDataSplitCharacter }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string market in markets)
            {
                BetfairMarket parsedMarket = ParseMarket(market);
                // only add to the collection if it is a match odds market.
                if (parsedMarket != null && parsedMarket.MarketName == MatchOddsMarketName)
                {
                    parsedMarkets.Add(parsedMarket);
                }
            }

            return parsedMarkets;
        }

        private BetfairMarket ParseMarket(string marketData)
        {
            // Replace any escaped characters with the placeholder value so that 
            // we dont split on the escaped character. A field may contain the character
            // '~' in its value.
            // We dont want to remove empty strings since it will change the index 
            // into the fields.
            string replacedString = ReplaceEscapedCharacters(marketData, MarketFieldSplitCharacter);


            string[] fields = marketData.Split(MarketFieldSplitCharacter);

            BetfairMarket market = null;

            try
            {
                market = new BetfairMarket();
                market.Id = int.Parse(fields[MarketFieldIndex.Id]);
                market.MarketName = fields[MarketFieldIndex.MarketName];
                market.Type = fields[MarketFieldIndex.Type];
                market.Status = fields[MarketFieldIndex.Status];
                market.Title = ParseMarketTitleFromMenuPath(fields[MarketFieldIndex.MenuPath]);
                // RakeshTODO: Check with Okash if double is big enough.
                double millisecondsSince1970 =  double.Parse(fields[MarketFieldIndex.EventDate]);
                market.EventDate = new DateTime(1970, 1, 1).AddMilliseconds(millisecondsSince1970); 
                market.RunnerCount =  int.Parse(fields[MarketFieldIndex.RunnerCount]);

            }
            catch (Exception exception)
            {
                // We dont want handle any exceptions and return null.
                // To prevent data corruption any errors during the parsing 
                // of a market will return the market as null so that it is 
                // excluded from any results.
                if (ExceptionHelper.IsSecurityOrCriticalException(exception))
                {
                    throw;
                }

                Debug.Fail("Unable to parse market data.", exception.ToString());
                // indicates that we could not parse the market.
                market = null;
            }

            return market;
        }

        private string ParseMarketTitleFromMenuPath(string menuPath)
        {
            // Example of menuPath: 
            // \Soccer\Scottish Soccer\Bells League Div 1\Fixtures 22 November \Partick v Clyde
            // Purpose of the method is to extract 'Partick v Clyde'
            string[] menuPathElements =
                menuPath.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            return menuPathElements[menuPathElements.Length - 1];
        }

        private struct MarketFieldIndex
        {
            internal const int Id = 0;
            internal const int MarketName = 1;
            internal const int Type = 2;
            internal const int Status = 3;
            internal const int EventDate = 4;
            internal const int MenuPath = 5;
            internal const int RunnerCount = 11;
        }
    }
}
