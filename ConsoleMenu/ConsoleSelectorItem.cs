using System;

namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelectorItem : IFormattable
    {
        public ConsoleSelectorItem() { }

        public ConsoleSelectorItem(Action action) => Action = action;

        public ConsoleSelectorItem(string title, string description = null, object tag = null, Action action = null)
        {
            Title = title;
            Description = description;
            Tag = tag;
            Action = action;
        }

        /// <summary>
        /// Главный текст элемента
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание элемента
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Объект связанный с элементом
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Действие, выполняемое при выборе этого элемента
        /// </summary>
        public Action Action { get; set; }

        public string ToString(string format, IFormatProvider formatProvider) => Title;
    }
}
