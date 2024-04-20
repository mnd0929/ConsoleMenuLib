using System.Diagnostics;

namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelectorSettings
    {
        /// <summary>
        /// Указывает, нужно ли скрывать меню после выбора
        /// </summary>
        public bool AutoHide { get; set; } = true;

        /// <summary>
        /// Максимальное колличество одновременно отображаемых элементов на экране. (-1 - Отображаются все элементы сразу)
        /// </summary>
        public int MaxHeight { get; set; } = -1;

        /// <summary>
        /// Указывает, требуется ли сброс индекса при отображении меню
        /// </summary>
        public bool ResetIndex { get; set; } = false;

        /// <summary>
        /// Указывает, требуется ли очистка элементов после выбора
        /// </summary>
        public bool ClearItemsAfterSelecting { get; set; } = false;

        /// <summary>
        /// Режим переключения страниц
        /// </summary>
        public PageSwitchMode SwitchMode { get; set; } = PageSwitchMode.ElementByElement;

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
        /// Префиксы элементов в меню
        /// </summary>
        public ConsoleSelectorPrefixes Prefixes { get; set; } = new ConsoleSelectorPrefixes();
    }

    public enum PageSwitchMode
    {
        /// <summary>
        /// Если индекс активного элемента выходит за пределы отображаемого меню
        /// страница сменится на следующую, а индекс на 0
        /// </summary>
        PageByPage,

        /// <summary>
        /// Если индекс активного элемента выходит за пределы отображаемого меню
        /// индекс останется прежним, а начальный и конечный индекс страницы изменятся на 1
        /// </summary>
        ElementByElement
    }
}
