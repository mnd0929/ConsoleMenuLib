using ConsoleToolsCollection.ConsoleSelector.Helpers;
using System;
using System.Collections.Generic;

namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelector
    {
        /// <summary>
        /// Элементы отображаемые в меню
        /// </summary>
        public List<ConsoleSelectorItem> Items { get; set; } = new List<ConsoleSelectorItem>();

        /// <summary>
        /// Цвета элементов в меню
        /// </summary>
        public ConsoleSelectorColors Colors { get; set; } = new ConsoleSelectorColors();

        /// <summary>
        /// Отступы элементов в меню
        /// </summary>
        public ConsoleSelectorIndentations Indentations { get; set; } = new ConsoleSelectorIndentations();

        /// <summary>
        /// Кнопки ответственные за управление в меню
        /// </summary>
        public ConsoleSelectorKeys Keys { get; set; } = new ConsoleSelectorKeys();

        /// <summary>
        /// Настройки меню
        /// </summary>
        public ConsoleSelectorSettings Settings { get; set; } = new ConsoleSelectorSettings();

        /// <summary>
        /// Индекс активного элемента
        /// </summary>
        public int SelectedItemIndex { get; set; } = 0;

        /// <summary>
        /// Колличество страниц (Если Settings.MaxHeight > 0)
        /// </summary>
        public int PagesCount { get; private set; } = 0;

        /// <summary>
        /// Текущая страница (Если Settings.MaxHeight > 0)
        /// </summary>
        public int CurrentPage { get; private set; } = 0;

        /// <summary>
        /// Показать меню, дождаться выбора и вернуть выбранный элемент
        /// </summary>
        /// <returns>Выбранный элемент</returns>
        public ConsoleSelectorItem ShowSelector()
        {
            Console.CursorVisible = false;

            int reservePosTop = Console.CursorTop;
            int reservePosLeft = Console.CursorLeft;

            while (true) 
            {
                Console.SetCursorPosition(reservePosLeft, reservePosTop);

                ShowPage();

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == Keys.Up && SelectedItemIndex - 1 >= 0)
                {
                    SelectedItemIndex--;
                }
                else if (key == Keys.Down && SelectedItemIndex + 1 < Items.Count)
                {
                    SelectedItemIndex++;
                }
                else if (key == Keys.Accept)
                {
                    if (Settings.HideMenuAfterSelecting)
                    {
                        Console.SetCursorPosition(reservePosLeft, reservePosTop);

                        HideSelector();

                        Console.SetCursorPosition(reservePosLeft, reservePosTop);
                    }

                    Console.CursorVisible = true;

                    Items[SelectedItemIndex].Action?.Invoke();

                    return Items[SelectedItemIndex];
                }
            }
        }

        private void ShowPage()
        {
            if (Settings.MaxHeight != -1 && Settings.MaxHeight != 0)
            {
                PagesCount = Items.Count % Settings.MaxHeight == 0 ? Items.Count / Settings.MaxHeight : Items.Count / Settings.MaxHeight + 1;
                CurrentPage = SelectedItemIndex / Settings.MaxHeight;

                ShowRange(CurrentPage * Settings.MaxHeight, CurrentPage * Settings.MaxHeight + Settings.MaxHeight);
            }
            else
            {
                ShowRange(0, Items.Count);
            }
        }

        private void ShowRange(int startIndex, int finishIndex)
        {
            for (int i = startIndex; i < finishIndex; i++)
            {
                if (i > Items.Count - 1 || i < 0)
                    break;

                bool isActive = SelectedItemIndex == i;

                string modifyedText = string.Concat(

                    // Отступ текста
                    Helpers.StringHelper.Repeat(Indentations.IndentationString, Indentations.TextIndentation),

                    // Начальные префиксы
                    Settings.DefaultPrefix, isActive ? Settings.ActivePrefix : Settings.NotActivePrefix,

                    // Текст
                    Items[i].Title,

                    // Конечные префиксы
                    Settings.FinalString);

                // Левый отступ
                if (Indentations.SelectionIndentationLeft != -1)
                    Console.SetCursorPosition(Indentations.SelectionIndentationLeft, Console.CursorTop);

                // Правый отступ
                if (Indentations.SelectionIndentationRight != -1 && Console.WindowWidth > Indentations.SelectionIndentationRight)
                    modifyedText += Helpers.StringHelper.Repeat(Indentations.IndentationString,
                        Console.WindowWidth - Indentations.SelectionIndentationRight - modifyedText.Length - Indentations.SelectionIndentationLeft);

                Helpers.ColorConsole.WriteLine(modifyedText,
                    isActive ? Colors.ActiveForegroundColor : Colors.DefaultForegroundColor,
                    isActive ? Colors.ActiveBackgroundColor : Colors.DefaultBackgroundColor);
            }
        }

        private void HideSelector()
        {
            for (int i = 0; i < Items.Count; i++)
                ColorConsole.WriteLine(Helpers.StringHelper.Repeat(" ", Console.WindowWidth));
        }
    }
}
