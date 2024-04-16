using System;
using System.IO;
using System.Linq;
using ConsoleToolsCollection.ConsoleSelector;

namespace ConsoleMenuTests
{
    internal class Program
    {
        static string CurrentDirectory = $"{Environment.CurrentDirectory[0]}:\\";

        static int AddressBarTop = Console.CursorTop + 1;
        static int AddressBarLeft = Console.CursorLeft + 1;

        static void Main(string[] args)
        {
            ShowAddressBar();

            ConsoleSelector consoleSelector = new ConsoleSelector
            {
                Settings = new ConsoleSelectorSettings
                {
                    MaxHeight = Console.WindowHeight - 5,
                    HideMenuAfterSelecting = true,
                    ClearItemsAfterSelecting = true,
                    ResetIndex = false,

                    Colors = new ConsoleSelectorItemColors
                    {
                        DefaultBackgroundColor = ConsoleColor.DarkBlue,
                        ActiveBackgroundColor = ConsoleColor.White,

                        DefaultForegroundColor = ConsoleColor.White,
                        ActiveForegroundColor = ConsoleColor.DarkBlue,
                    },

                    Indentations = new ConsoleSelectorIndentations
                    {
                        SelectionRight = 1,
                        SelectionLeft = 1,
                        Text = 3
                    }
                },
            };

            while (true) 
            {
                ShowAddressBar();

                consoleSelector.Items.Add(new ConsoleSelectorItem 
                {
                    Title = "..",
                    Tag = PathUp(CurrentDirectory),
                    CustomColors = new ConsoleSelectorItemColors
                    {
                        DefaultForegroundColor = ConsoleColor.DarkYellow
                    }
                });

                try
                {
                    foreach (string str in Directory.EnumerateDirectories(CurrentDirectory))
                    {
                        consoleSelector.Items.Add(BuildPathItem(str, ConsoleColor.DarkYellow));
                    }

                    foreach (string str in Directory.EnumerateFiles(CurrentDirectory))
                    {
                        consoleSelector.Items.Add(BuildPathItem(str, consoleSelector.Settings.Colors.DefaultForegroundColor));
                    }
                }
                catch 
                {
                    CurrentDirectory = PathUp(CurrentDirectory);
                }

                ConsoleSelectorItem consoleSelectorItem = consoleSelector.Show();

                if (Directory.Exists(consoleSelectorItem.Tag as string))
                    CurrentDirectory = consoleSelectorItem.Tag as string;
            }
        }

        static string PathUp(string path) => Path.GetDirectoryName(path);

        static ConsoleSelectorItem BuildPathItem(string path, ConsoleColor foreColor)
        {
            return new ConsoleSelectorItem
            {
                Title = Path.GetFileName(path),
                Tag = path,
                CustomColors = new ConsoleSelectorItemColors
                {
                    DefaultForegroundColor = foreColor
                }
            };
        }

        static void ShowAddressBar()
        {
            string address = string.Concat(CurrentDirectory, string.Concat(Enumerable.Repeat(" ", Console.WindowWidth - CurrentDirectory.Length > 0 ? Console.WindowWidth - CurrentDirectory.Length : 0)));

            Console.SetCursorPosition(AddressBarLeft, AddressBarTop);
            Console.WriteLine(address);
        }
    }
}
