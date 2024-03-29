using System.Linq;

namespace ConsoleToolsCollection.ConsoleSelector.Helpers
{
    internal static class StringHelper
    {
        public static string Repeat(string str, int count) => 
            string.Concat(Enumerable.Repeat(str, count));
    }
}
