using System;

namespace ConsoleToolsCollection.ConsoleSelector.Helpers
{
    internal class ColorConsole
    {
        public static string ReadLine(ConsoleColor foreColor = ConsoleColor.White)
        {
            ConsoleColor consoleForeReserveColor = Console.ForegroundColor;

            Console.ForegroundColor = foreColor;
            string output = Console.ReadLine();
            Console.ForegroundColor = consoleForeReserveColor;

            return output;
        }

        public static void WriteLine(string text, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            ConsoleColor consoleForeReserveColor = Console.ForegroundColor;
            ConsoleColor consoleBackReserveColor = Console.BackgroundColor;

            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.WriteLine(text);
            Console.ForegroundColor = consoleForeReserveColor;
            Console.BackgroundColor = consoleBackReserveColor;
        }

        public static void Write(string text, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            ConsoleColor consoleForeReserveColor = Console.ForegroundColor;
            ConsoleColor consoleBackReserveColor = Console.BackgroundColor;

            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.Write(text);
            Console.ForegroundColor = consoleForeReserveColor;
            Console.BackgroundColor = consoleBackReserveColor;
        }
    }
}
