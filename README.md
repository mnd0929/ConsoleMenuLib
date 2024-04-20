# ConsoleMenu

https://github.com/mnd0929/ConsoleMenuLib/assets/92184643/b7492c6e-b52a-4bd6-9850-c44841504504

**Создание меню**
```csharp
ConsoleSelector consoleSelector = new ConsoleSelector
{
    Settings = new ConsoleSelectorSettings
    {
        MaxHeight = 20 // Максимальное колличество одновременно отображаемых элементов на экране. (По умолчанию ображаются все элементы сразу)

        Indentations = new ConsoleSelectorIndentations
        {
            SelectionRight = 20,
            SelectionLeft = 20,
            Text = 3
        },
        Keys = new ConsoleSelectorKeys 
        {
            Up = ConsoleKey.W,
            Down = ConsoleKey.S,
            Accept = ConsoleKey.Enter
        }
    },
};
```


**Добавление элементов**
```csharp
consoleSelector.Items.Add(new ConsoleSelectorItem($"GitHub", action: () =>
{
    Process.Start("https://github.com/");
}));
```
```csharp
consoleSelector.Items.Add(new ConsoleSelectorItem 
{
    Title = "GitHub",
    Description = "Открыть GitHub",
    Tag = "https://github.com/"
});
```


**Отображение меню и получение выбранного элемента**
```csharp
ConsoleSelectorItem consoleSelectorItem = consoleSelector.Show();
```
