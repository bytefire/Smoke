using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Smoke.Utilities.Aliases
{
    /// <summary>
    /// Contains methods to manipulate the runner aliases xml file.
    /// </summary>
    public class RunnerAliasesHelper
    {
        private const string RunnerAliasesFilePath = @"D:\Development\Smoke\Source\Smoke\Files\RunnerAliases.xml";
        private const string RootTagName = "RunnerAliases";
        private const string RunnerTagName = "Runner";
        private const string AliasTagName = "Alias";
        private const string RunnerAttributeName = "name";

        private List<NameAliases> _cachedNameAliases = new List<NameAliases>();
        /// <summary>
        /// Adds the specified runner if not already added. Returns false if runner is already added.
        /// Also adds the same runner name as first alias by default.
        /// </summary>
        /// <param name="runnerName"></param>
        public static bool AddRunner(string runnerName)
        {
            XDocument aliases = XDocument.Load(RunnerAliasesFilePath);
            // IMPORTANT: Always convert to lower case when comparing runner-aliases strings.
            bool hasRunner = aliases.Descendants(RunnerTagName).Any(item => item.Attribute(RunnerAttributeName).Value.ToLower() == runnerName.ToLower());
            if (hasRunner)
            {
                return false;
            }
            aliases.Root.Add(new XElement(RunnerTagName,
                            new XAttribute(RunnerAttributeName, runnerName),
                            new XElement(AliasTagName, runnerName)));
            aliases.Save(RunnerAliasesFilePath);
            return true;
        }
        /// <summary>
        /// Adds alias for the specified runner. If that runner already has that alias then 
        /// this method returns false.
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <param name="runner">The runner whose alias it is.</param>
        public static bool AddAlias(string alias, string runner)
        {
            XDocument aliases = XDocument.Load(RunnerAliasesFilePath);
            // IMPORTANT: Always convert to lower case when comparing runner-aliases strings.
            var runnerElement = aliases.Descendants(RunnerTagName).Where(
                item => item.Attribute(RunnerAttributeName).Value.ToLower() == runner.ToLower()).Single();
            bool hasAlias = runnerElement.Descendants(AliasTagName).Where(item => item.Value.ToLower() == alias.ToLower()).Any();
            if (hasAlias)
            {
                return false;
            }
            runnerElement.Add(new XElement(AliasTagName, alias));
            aliases.Save(RunnerAliasesFilePath);
            return true;
        }
        /// <summary>
        /// Returns a collection of all runners with their aliases.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<string>> LoadAllRunners()
        {
            XDocument aliases = XDocument.Load(RunnerAliasesFilePath);
            Dictionary<string, List<string>> runnersDictionary = new Dictionary<string, List<string>>();
            Parallel.ForEach(aliases.Descendants(RunnerTagName), item =>
            {
                runnersDictionary.Add(item.Attribute(RunnerAttributeName).Value,
                    new List<string>(from alias in item.Descendants(AliasTagName) select alias.Value));
            });
            return runnersDictionary;
        }

        /// <summary>
        /// Gets the runner name and all its aliases for the first matching alias.
        /// </summary>
        /// <param name="alias">Alias to search for.</param>
        /// <returns></returns>
        public static NameAliases GetNameAliasesForAlias(string alias)
        {
            XDocument aliases = XDocument.Load(RunnerAliasesFilePath);
            IEnumerable<NameAliases> nameAliases = from item in aliases.Descendants(RunnerTagName)
                                                   where
                                                       (item.Descendants(AliasTagName).Any(aliasItem => aliasItem.Value.ToLower() == alias.ToLower()))
                                                   select new NameAliases()
                                                   {
                                                       RunnerName = item.Attribute(RunnerAttributeName).Value,
                                                       Aliases = new List<string>(from aliasItems in item.Descendants(AliasTagName) select aliasItems.Value)
                                                   };
            return nameAliases.SingleOrDefault();
        }

        /// <summary>
        /// Gets runner name and all aliases for the given runner name.
        /// </summary>
        /// <param name="runnerName">The runner name to search for.</param>
        /// <returns></returns>
        public static NameAliases GetNameAliasesForRunner(string runnerName)
        {
            XDocument aliases = XDocument.Load(RunnerAliasesFilePath);
            IEnumerable<NameAliases> nameAliases = from item in aliases.Descendants(RunnerTagName)
                                                   where (item.Attribute(RunnerAttributeName).Value.ToLower() == runnerName.ToLower())
                                                   select new NameAliases()
                                                   {
                                                       RunnerName = item.Attribute(RunnerAttributeName).Value,
                                                       Aliases = new List<string>(from aliasItems in item.Descendants(AliasTagName) select aliasItems.Value)
                                                   };

            return nameAliases.SingleOrDefault();
        }

        public static void CreateFile()
        {
            XDocument aliases = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(RootTagName,
                        new XElement(RunnerTagName,
                            new XAttribute(RunnerAttributeName, "Manchester Utd FC"),
                            new XElement(AliasTagName, "Man Utd"),
                            new XElement(AliasTagName, "Man U"),
                            new XElement(AliasTagName, "MUFC")),
                        new XElement(RunnerTagName,
                            new XAttribute(RunnerAttributeName, "Real Madrid FC"),
                            new XElement(AliasTagName, "Real Madrid"))));
            aliases.Save(RunnerAliasesFilePath);
        }
    }
}
