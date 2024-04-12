using System;

namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelectorItem : IFormattable
    {
        public ConsoleSelectorItem() { }

        public ConsoleSelectorItem(Action action) => Action = action;

        public ConsoleSelectorItem(object obj) => (Tag, Title) = (obj, obj as string);

        public ConsoleSelectorItem(
            string title, 
            string description = null, 
            object tag = null, 
            Action action = null, 
            ConsoleSelectorItemColors customColors = null)
        {
            Title = title;
            Description = description;
            Tag = tag;
            Action = action;
            CustomColors = customColors;
        }

        /// <summary>
        /// Индивидуальный цвет элемента
        /// </summary>
        public ConsoleSelectorItemColors CustomColors { get; set; }

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
