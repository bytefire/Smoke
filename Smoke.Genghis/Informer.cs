using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Genghis
{
    public class Informer
    {
        private static decimal bestWorstPayoff = Decimal.MinValue;
        public static void Inform(OptimumValues optimumValues)
        {
            bool isNewBest = false;
            if (optimumValues.WorstPayoff > bestWorstPayoff)
            {
                bestWorstPayoff = optimumValues.WorstPayoff;
                isNewBest = true;
            }
            LogValues(optimumValues, isNewBest);
            // OkashTODO: check based on worst payoff and time since last notification whether to notify.
            //            if to notify then send push notification and update local variables.
        }

        private static void LogValues(OptimumValues optimumValues, bool isNewBest)
        {
            string fileName = GetFullNameOfLogFile(optimumValues.GetMatchTitle());
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                string line = "[" + DateTime.Now.ToString() + "] " + optimumValues.WorstPayoff.ToString() +
                    " - " + optimumValues.GetNotificationString();
                if (isNewBest)
                {
                    line += " (£££)";
                }
                writer.WriteLine(line);
            }
        }

        private static string GetFullNameOfLogFile(string matchTitle)
        {
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fileName = String.Format("{0}.txt", matchTitle);
            return Path.Combine(folderPath, "Genghis", fileName);
        }
    }
}
