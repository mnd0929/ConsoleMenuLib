namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelectorIndentations
    {
        /// <summary>
        /// Строка использующаяся для отступов
        /// </summary>
        public string RepeatingLine { get; set; } = " ";

        /// <summary>
        /// Отступ текста
        /// </summary>
        public int Text { get; set; } = 0;

        /// <summary>
        /// Отступ выделения текста слева
        /// </summary>
        public int SelectionLeft { get; set; } = -1;

        /// <summary>
        /// Отступ выделения текста справа. Значение -1 указывает, что выделение будет заканчиваться вместе с текстом
        /// </summary>
        public int SelectionRight { get; set; } = -1;
    }
}
