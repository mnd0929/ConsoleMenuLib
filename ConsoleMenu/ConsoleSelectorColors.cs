using System;

namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelectorColors
    {
        /// <summary>
        /// Цвет текста активного элемента
        /// </summary>
        public ConsoleColor ActiveForegroundColor { get; set; } = ConsoleColor.Black;

        /// <summary>
        /// Цвет фона активного элемента
        /// </summary>
        public ConsoleColor ActiveBackgroundColor { get; set; } = ConsoleColor.Gray;

        /// <summary>
        /// Цвет текста неактивного элемента
        /// </summary>
        public ConsoleColor DefaultForegroundColor { get; set; } = ConsoleColor.Gray;

        /// <summary>
        /// Цвет фона неактивного элемента
        /// </summary>
        public ConsoleColor DefaultBackgroundColor { get; set; } = ConsoleColor.Black;
    }
}
