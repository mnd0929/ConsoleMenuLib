using System.Linq;

namespace ConsoleToolsCollection.ConsoleSelector.Helpers
{
    internal static class StringHelper
    {
        public static string Repeat(string str, int count) => 
            string.Concat(Enumerable.Repeat(str, count));

        public static string TruncateString(string str, int maxChars)
        {
            if (str.Length > maxChars)
                return string.Concat(str.Substring(0, str.Length - (str.Length - maxChars + 3)), "...");

            return str;
        }
    }
}
