using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CompanyName.ProductName.Provider.Betfair
{
    internal class Parser
    {
        private const string EscapedCharacterPlaceholder = @"#*£*#";

        protected static string ReplaceEscapedCharacters(string data, char splitCharacter)
        {
            // First replace all escaped characters with EscapedCharacterPlaceholder
            string escapedCharacter = string.Format(CultureInfo.InvariantCulture, @"\{0}", splitCharacter);

            // Replace any escaped characters with the placeholder value so that 
            // we dont split on the escaped character. 
            string replacedString = data.Replace(escapedCharacter, EscapedCharacterPlaceholder);
            return replacedString;
        }
    }
}
