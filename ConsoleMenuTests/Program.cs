using System;
using System.Threading;
using ConsoleToolsCollection.ConsoleSelector;

namespace ConsoleMenuTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("МАНКАРАФТ 2D ОНЛАЙН БЕСПЛАТНО БЕЗ РЕГИСТРАЦИИ И СМС ИГРАТЬ С ВИРУСАМИ И БОМЖАМИ: \r\n");

            ConsoleSelector consoleSelector = new ConsoleSelector
            {
                Indentations = new ConsoleSelectorIndentations
                {
                    SelectionIndentationRight = 20,
                    SelectionIndentationLeft = 20,
                    TextIndentation = 3
                },
                Settings = new ConsoleSelectorSettings
                {
                    MaxHeight = 20
                }
            };

            consoleSelector.Items.Add(new ConsoleSelectorItem($"Играть", action: () =>
            {
                Console.WriteLine("Хрен тебе, а не игрушечки-погремушечки");
            }));

            consoleSelector.Items.Add(new ConsoleSelectorItem($"Настройки", action: () =>
            {
                Console.WriteLine("Мозги себе настрой");
            }));

            consoleSelector.Items.Add(new ConsoleSelectorItem($"Выход", action: () =>
            {
                Console.WriteLine("Ну и пшел ты нафег");

                Thread.Sleep(1000);

                Environment.Exit(0);
            }));

            consoleSelector.ShowSelector();
        }
    }
}
