namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelectorIndentations
    {
        /// <summary>
        /// Строка использующаяся для отступов
        /// </summary>
        public string IndentationString { get; set; } = " ";

        /// <summary>
        /// Отступ текста
        /// </summary>
        public int TextIndentation { get; set; } = 0;

        /// <summary>
        /// Отступ выделения текста слева
        /// </summary>
        public int SelectionIndentationLeft { get; set; } = -1;

        /// <summary>
        /// Отступ выделения текста справа. Значение -1 указывает, что выделение будет заканчиваться вместе с текстом
        /// </summary>
        public int SelectionIndentationRight { get; set; } = -1;
    }
}
