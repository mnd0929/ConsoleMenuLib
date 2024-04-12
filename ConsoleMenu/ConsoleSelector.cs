using System.Collections.Generic;
using System;

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
        public ConsoleSelectorItemColors Colors { get; set; } = new ConsoleSelectorItemColors();

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
        public int CurrentPage { get; set; } = 0;

        /// <summary>
        /// Показать меню, дождаться выбора и вернуть выбранный элемент
        /// </summary>
        /// <returns>Выбранный элемент</returns>
        public ConsoleSelectorItem Show()
        {
            SelectedItemIndex = 0;

            Console.CursorVisible = false;

            int reservePosTop = Console.CursorTop;
            int reservePosLeft = Console.CursorLeft;

            while (true)
            {
                Console.SetCursorPosition(reservePosLeft, reservePosTop);

                ShowPage();

                ConsoleKey key = Console.ReadKey(true).Key;

                // Переключение активного элемента
                if (key == Keys.Up && SelectedItemIndex - 1 >= 0)
                {
                    SelectedItemIndex--;
                }
                else if (key == Keys.Down && SelectedItemIndex + 1 < Items.Count)
                {
                    SelectedItemIndex++;
                }

                // Переключение страниц
                else if (key == Keys.Right)
                {
                    int correctIndex = SelectedItemIndex + Settings.MaxHeight;
                    int maxIndex = Items.Count - 1;

                    SelectedItemIndex = correctIndex < maxIndex ? correctIndex : maxIndex;
                }
                else if (key == Keys.Left)
                {
                    int correctIndex = SelectedItemIndex - Settings.MaxHeight;
                    int minIndex = 0;

                    SelectedItemIndex = correctIndex >= minIndex ? correctIndex : minIndex;
                }

                // Выбор элемента
                else if (key == Keys.Accept)
                {
                    if (Settings.HideMenuAfterSelecting)
                        Hide(reservePosLeft, reservePosTop);

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

                int startIndex = CurrentPage * Settings.MaxHeight;
                int finishIndex = startIndex + Settings.MaxHeight;

                ShowRange(startIndex, finishIndex);
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
                {
                    Console.Write(string.Concat("\r", Helpers.StringHelper.Repeat(" ", Console.WindowWidth)));

                    continue;
                }

                int maxTextLength = Console.WindowWidth - Indentations.SelectionRight - Indentations.SelectionLeft;

                bool isActive = SelectedItemIndex == i;

                string modifyedText = string.Concat(

                    // Отступ текста
                    Helpers.StringHelper.Repeat(Indentations.RepeatingLine, Indentations.Text),

                    // Начальные префиксы
                    Settings.DefaultPrefix, isActive ? Settings.ActivePrefix : Settings.NotActivePrefix,

                    // Текст
                    Items[i].Title,

                    // Конечные префиксы
                    Settings.FinalString);

                // Сокращение строки
                modifyedText = Helpers.StringHelper.TruncateString(modifyedText, maxTextLength);

                // Левый отступ
                if (Indentations.SelectionLeft != -1)
                    Console.SetCursorPosition(Indentations.SelectionLeft, Console.CursorTop);

                // Правый отступ
                if (Indentations.SelectionRight != -1 && Console.WindowWidth > Indentations.SelectionRight)
                    modifyedText += Helpers.StringHelper.Repeat(Indentations.RepeatingLine,
                        Console.WindowWidth - Indentations.SelectionRight - modifyedText.Length - Indentations.SelectionLeft);

                ConsoleColor? foreColor = isActive ? Colors.ActiveForegroundColor : Colors.DefaultForegroundColor;
                ConsoleColor? backColor = isActive ? Colors.ActiveBackgroundColor : Colors.DefaultBackgroundColor;

                ConsoleSelectorItemColors customColors = Items[i].CustomColors;

                if (customColors != null)
                {
                    (foreColor, backColor) = isActive ?
                        (customColors.ActiveForegroundColor, customColors.ActiveBackgroundColor) : 
                        (customColors.DefaultForegroundColor, customColors.DefaultBackgroundColor);
                }

                Helpers.ColorConsole.WriteLine(modifyedText, foreColor.Value, backColor.Value);
            }
        }
        public void Hide(int reservePosLeft, int reservePosTop)
        {
            Console.SetCursorPosition(reservePosLeft, reservePosTop);

            string line = string.Concat('\r', Helpers.StringHelper.Repeat(" ", Console.WindowWidth));
            string text = Helpers.StringHelper.Repeat(line, Settings.MaxHeight);

            Console.Write(text);

            Console.SetCursorPosition(reservePosLeft, reservePosTop);
        }
    }
}
