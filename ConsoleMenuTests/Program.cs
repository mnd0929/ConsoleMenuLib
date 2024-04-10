using System;
using System.Linq;
using ConsoleToolsCollection.ConsoleSelector;
using ConsoleToolsCollection.ConsoleSelector.Editors;

namespace ConsoleMenuTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleSelector consoleSelector = new ConsoleSelector
            {
                Indentations = new ConsoleSelectorIndentations
                {
                    SelectionRight = 20,
                    SelectionLeft = 20,
                    Text = 3
                },
                Settings = new ConsoleSelectorSettings
                {
                    MaxHeight = 20,
                    HideMenuAfterSelecting = true,
                }
            };

            for (int i = 0; i < 23; i++)
            {
                consoleSelector.Items.Add(new ConsoleSelectorItem($"{i} // {string.Concat(Enumerable.Repeat("-", 100))}"));
            }

            consoleSelector.Show();
        }
    }
}
