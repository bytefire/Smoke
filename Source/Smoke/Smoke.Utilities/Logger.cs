using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Utilities
{
    public class Logger
    {
        public static void Write(string projectName, string fileNameWithoutExtension, string statement)
        {
            string fullPath = GetLogFilePath(projectName, fileNameWithoutExtension);
            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                string line = "[" + DateTime.Now.ToString() + "] " + statement;
                writer.WriteLine(line);
            }
        }

        private static string GetLogFilePath(string projectName, string fileNameWithoutExtension)
        {
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fileName = String.Format("{0}.txt", fileNameWithoutExtension);
            return Path.Combine(folderPath, projectName, fileName);
        }
    }
}
