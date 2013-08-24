using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Tamerlane
{
    class Program
    {
        private const string DateTimeFormat = "yyyyMMddHHmmss";
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                Console.WriteLine("***" + arg);
            }
            InitialiseAndStartManager(args);
        }

        private static void InitialiseAndStartManager(string[] args)
        {
            string marketId = args[0];
            string chosenRunnerId = args[1];
            decimal chosenOdds = decimal.Parse(args[2]);
            decimal chosenAmount = decimal.Parse(args[3]);
            decimal budget = decimal.Parse(args[4]);
            DateTime startingTime = DateTime.ParseExact(args[5], DateTimeFormat, null);
            Manager manager = new Manager()
            {
                Budget = budget,
                ChosenAmount = chosenAmount,
                ChosenOdds = chosenOdds,
                ChosenRunnerId = chosenRunnerId,
                MarketId = marketId,
                StartingTime = startingTime
            };
            manager.Start();
        }
    }
}
