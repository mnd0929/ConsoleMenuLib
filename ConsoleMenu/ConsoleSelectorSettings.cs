namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelectorSettings
    {
        /// <summary>
        /// Указывает, нужно ли скрывать меню после выбора
        /// </summary>
        public bool HideMenuAfterSelecting { get; set; } = true;

        /// <summary>
        /// Максимальное колличество одновременно отображаемых элементов на экране. (-1 - Отображаются все элементы сразу)
        /// </summary>
        public int MaxHeight { get; set; } = -1;

        /// <summary>
        /// Стандартный префикс для всех элементов
        /// </summary>
        public string DefaultPrefix { get; set; } = null;

        /// <summary>
        /// Префикс активного элемента
        /// </summary>
        public string ActivePrefix { get; set; } = null;

        /// <summary>
        /// Префикс неактивного элемента
        /// </summary>
        public string NotActivePrefix { get; set; } = null;

        /// <summary>
        /// Строка идущая после основного текста каждого элемента
        /// </summary>
        public string FinalString { get; set; } = null;
    }
}
