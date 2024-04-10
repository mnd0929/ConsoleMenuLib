using System;
using System.Reflection;

namespace ConsoleToolsCollection.ConsoleSelector.Editors
{
    public class ModelEditor
    {
        public ConsoleSelectorItem ShowSelector(ConsoleSelector consoleSelector, Type dataType, object data)
        {
            consoleSelector.Items.Clear();

            foreach (PropertyInfo field in dataType.GetProperties(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
            {
                consoleSelector.Items.Add(
                    new ConsoleSelectorItem($"{field.PropertyType.Name} {field.Name} = \"{field.GetValue(data)}\"", field.GetType().Name, field));
            }

            consoleSelector.Items.Add(new ConsoleSelectorItem("[/] Сохранить", tag: "save", action: () => 
            {
                
            }));

            return consoleSelector.Show();
        }
    }
}
