using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Utilities.Aliases
{
    /// <summary>
    /// Represents runner name and its associated aliases.
    /// </summary>
    public class NameAliases
    {
        public string RunnerName
        {
            get;
            set;
        }
        public List<string> Aliases
        {
            get;
            set;
        }
    }
}
