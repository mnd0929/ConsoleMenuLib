namespace ConsoleToolsCollection.ConsoleSelector
{
    public class ConsoleSelectorPrefixes
    {
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
