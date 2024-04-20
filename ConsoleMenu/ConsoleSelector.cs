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
        /// Настройки меню
        /// </summary>
        public ConsoleSelectorSettings Settings { get; set; } = new ConsoleSelectorSettings();

        /// <summary>
        /// Индекс активного элемента
        /// </summary>
        public int SelectedItemIndex { get; set; } = 0;

        /// <summary>
        /// Колличество страниц (Если Settings.MaxHeight > 0 && SwitchMode == PageSwitchMode.PageByPage)
        /// </summary>
        public int PagesCount { get; private set; } = 0;

        /// <summary>
        /// Текущая страница (Если Settings.MaxHeight > 0 && SwitchMode == PageSwitchMode.PageByPage)
        /// </summary>
        public int CurrentPage { get; set; } = 0;

        /// <summary>
        /// Смещение первого и последнего индекса страницы
        /// </summary>
        public int PageOffset { get; private set; } = 0;

        /// <summary>
        /// Показать меню, дождаться выбора и вернуть выбранный элемент
        /// </summary>
        /// <returns>Выбранный элемент</returns>
        public ConsoleSelectorItem Show()
        {
            CorrectSelectedItemIndex();

            if (Settings.ResetIndex)
                SetSelectedItemIndex(0);

            Console.CursorVisible = false;

            int reservePosTop = Console.CursorTop;
            int reservePosLeft = Console.CursorLeft;

            while (true)
            {
                Console.SetCursorPosition(reservePosLeft, reservePosTop);

                ShowPage();

                ConsoleKey key = Console.ReadKey(true).Key;

                // Переключение активного элемента
                if (key == Settings.Keys.Up)
                {
                    SetSelectedItemIndex(SelectedItemIndex - 1);
                }
                else if (key == Settings.Keys.Down)
                {
                    SetSelectedItemIndex(SelectedItemIndex + 1);
                }

                // Переключение страниц
                else if (key == Settings.Keys.Right)
                {
                    int correctIndex = SelectedItemIndex + Settings.MaxHeight;
                    int maxIndex = Items.Count - 1;

                    SetSelectedItemIndex(correctIndex < maxIndex ? correctIndex : maxIndex);
                }
                else if (key == Settings.Keys.Left)
                {
                    int correctIndex = SelectedItemIndex - Settings.MaxHeight;
                    int minIndex = 0;

                    SetSelectedItemIndex(correctIndex >= minIndex ? correctIndex : minIndex);
                }

                // Выбор элемента
                else if (key == Settings.Keys.Accept)
                {
                    ConsoleSelectorItem selectedItem = Items[SelectedItemIndex];

                    Console.CursorVisible = true;

                    if (Settings.AutoHide)
                        Hide(reservePosLeft, reservePosTop);

                    if (Settings.ClearItemsAfterSelecting)
                        Items.Clear();

                    selectedItem.Action?.Invoke();

                    return selectedItem;
                }

                //else SelectedItemIndex = GetIndexByFirstChar(key.ToString()[0]);
            }
        }

        private void ShowPage()
        {
            if (Settings.MaxHeight != -1 && Settings.MaxHeight != 0)
            {
                int startIndex = 0;
                int finishIndex = Items.Count - 1;

                switch (Settings.SwitchMode)
                {
                    case PageSwitchMode.PageByPage:
                        {
                            PagesCount = Items.Count % Settings.MaxHeight == 0 ? Items.Count / Settings.MaxHeight : Items.Count / Settings.MaxHeight + 1;
                            CurrentPage = SelectedItemIndex / Settings.MaxHeight;

                            startIndex = CurrentPage * Settings.MaxHeight;
                            finishIndex = startIndex + Settings.MaxHeight;
                        }
                        break;

                    case PageSwitchMode.ElementByElement:
                        {
                            if (SelectedItemIndex < PageOffset)
                                PageOffset--;

                            if (SelectedItemIndex > PageOffset + Settings.MaxHeight - 1)
                                PageOffset++;

                            startIndex = PageOffset;
                            finishIndex = Settings.MaxHeight + PageOffset;
                        }
                        break;
                }

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

                int maxTextLength = Console.WindowWidth - Settings.Indentations.SelectionRight - Settings.Indentations.SelectionLeft;

                bool isActive = SelectedItemIndex == i;

                string modifyedText = string.Concat(

                    // Отступ текста
                    Helpers.StringHelper.Repeat(Settings.Indentations.RepeatingLine, Settings.Indentations.Text),

                    // Начальные префиксы
                    Settings.Prefixes.DefaultPrefix, isActive ? Settings.Prefixes.ActivePrefix : Settings.Prefixes.NotActivePrefix,

                    // Текст
                    Items[i].Title,

                    // Конечные префиксы
                    Settings.Prefixes.FinalString);

                // Сокращение строки
                modifyedText = Helpers.StringHelper.TruncateString(modifyedText, maxTextLength);

                // Левый отступ
                if (Settings.Indentations.SelectionLeft != -1)
                    Console.SetCursorPosition(Settings.Indentations.SelectionLeft, Console.CursorTop);

                // Правый отступ
                if (Settings.Indentations.SelectionRight != -1 && Console.WindowWidth > Settings.Indentations.SelectionRight)
                    modifyedText += Helpers.StringHelper.Repeat(Settings.Indentations.RepeatingLine,
                        Console.WindowWidth - Settings.Indentations.SelectionRight - modifyedText.Length - Settings.Indentations.SelectionLeft);

                ConsoleColor? foreColor = isActive ? Settings.Colors.ActiveForegroundColor : Settings.Colors.DefaultForegroundColor;
                ConsoleColor? backColor = isActive ? Settings.Colors.ActiveBackgroundColor : Settings.Colors.DefaultBackgroundColor;

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

        public void SetSelectedItemIndex(int index)
        {
            SelectedItemIndex = index;

            CorrectSelectedItemIndex();
        }

        private void CorrectSelectedItemIndex()
        {
            if (SelectedItemIndex < 0)
            {
                SelectedItemIndex = 0;
            }
            else if (SelectedItemIndex > Items.Count - 1)
            {
                SelectedItemIndex = Items.Count - 1;
            }
        }

        private int GetIndexByFirstChar(char c)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Title[0] == c)
                    return i;
            }

            return 0;
        }
    }
}
