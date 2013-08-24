using CompanyName.ProductName.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyName.ProductName.Provider.Betfair
{
    internal class RunnerOddsInfoDataParser : Parser
    {
        private const char RunnerDataSplitCharacter = ':';

        internal Dictionary<int, ExchangePrice> ParseOddsInfo(string oddsData)
        {
            Dictionary<int, ExchangePrice> oddsIndexedByRunner = new Dictionary<int, ExchangePrice>();

            string replacedString = ReplaceEscapedCharacters(oddsData, RunnerDataSplitCharacter);
            string[] splitOnColon = replacedString.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            
            // Market Id string. Not being used at the moment.
            // string marketIdString = splitOnColon[0].Substring(0, splitOnColon[0].IndexOf("~"));

            string runnerIdString;
            string[] splitOnPipe;
            string backPrices;
            string[] splitOnTilde;

            // For each runner
            for (int i = 1; i < splitOnColon.Length; i++)
            {
                runnerIdString = splitOnColon[i].Substring(0, splitOnColon[i].IndexOf("~"));
                splitOnPipe = splitOnColon[i].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                backPrices = splitOnPipe[1];
                splitOnTilde = backPrices.Split(new string[] { "~" }, StringSplitOptions.RemoveEmptyEntries);
                decimal maxBackPrice = 0.0M, hold, backAmountAvailable = 0.0M;
                for (int j = 0; j < splitOnTilde.Length; j += 5)
                {
                    // first check back amount available
                    if (decimal.TryParse(splitOnTilde[j + 1], out backAmountAvailable))
                    {
                        // if back amount is zero or less then skip this price
                        if (backAmountAvailable <= 0.0M)
                        {
                            continue;
                        }
                    }
                    if (decimal.TryParse(splitOnTilde[j], out hold))
                    {
                        if (maxBackPrice < hold)
                        {
                            maxBackPrice = hold;
                        }
                    }
                }

                int runnerId = int.Parse(runnerIdString);
                oddsIndexedByRunner.Add(runnerId, new ExchangePrice(maxBackPrice));
            }

            return oddsIndexedByRunner;
        }
    }
}
