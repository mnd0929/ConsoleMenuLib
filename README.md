**Документация актуальна для версии 1.3.0**

# Отступы

```csharp
Indentations = new ConsoleSelectorIndentations
{
    SelectionLeft = 10,  // 1
    SelectionRight = 10, // 2
    Text = 3             // 3
}
```
![IndentationsDemo](https://github.com/mnd0929/ConsoleMenuLib/assets/92184643/7b63caf3-d53b-446b-8ea2-12783da66224)

# Префиксы
```csharp
Prefixes = new ConsoleSelectorPrefixes 
{
    ActivePrefix = " <1> ",
    DefaultPrefix = " <2> ",
    NotActivePrefix = " <3> ",
    FinalString = " <4> "
}
```
![PrefixesDemo](https://github.com/mnd0929/ConsoleMenuLib/assets/92184643/e035c721-8216-4d40-bce4-28017c5a7f37)

# Цвета
```csharp
Colors = new ConsoleSelectorItemColors
{
    DefaultBackgroundColor = ConsoleColor.DarkBlue,
    ActiveBackgroundColor = ConsoleColor.White,

    DefaultForegroundColor = ConsoleColor.White,
    ActiveForegroundColor = ConsoleColor.DarkBlue,
}
```
![ColorsDemo](https://github.com/mnd0929/ConsoleMenuLib/assets/92184643/c6cb92d1-b706-42a5-aa41-83979e0bf66c)


# Режимы переключения страниц

```csharp
SwitchMode = PageSwitchMode.ElementByElement
```
https://github.com/mnd0929/ConsoleMenuLib/assets/92184643/b7492c6e-b52a-4bd6-9850-c44841504504

```csharp
SwitchMode = PageSwitchMode.PageByPage
```
https://github.com/mnd0929/ConsoleMenuLib/assets/92184643/6a421fd6-bcc2-42ca-b071-02c40ffa7a70

# Создание меню
```csharp
ConsoleSelector consoleSelector = new ConsoleSelector
{
    Settings = new ConsoleSelectorSettings
    {
        MaxHeight = Console.WindowHeight - 5,
        ClearItemsAfterSelecting = true,
        ResetIndex = false,                    // Выделение элемента остается после выбора
        AutoHide = true,                       // Меню автоматически скрывается с консоли после выбора

        Indentations = new ConsoleSelectorIndentations
        {
            SelectionRight = 10,
            SelectionLeft = 10,
            Text = 3
        },

        SwitchMode = PageSwitchMode.ElementByElement
    },
};
```


# Добавление элементов
```csharp
consoleSelector.Items.Add(new ConsoleSelectorItem($"GitHub", action: () =>
{
    Process.Start("https://github.com/");
}));
```
```csharp
ConsoleSelectorItem githubItem = new ConsoleSelectorItem 
{
    Title = "GitHub",
    Description = "Открыть GitHub",
    Tag = "https://github.com/",
    CustomColors = new ConsoleSelectorItemColors
    {
        DefaultForegroundColor = ConsoleColor.Blue
    }
};

consoleSelector.Items.Add(githubItem);
```


# Отображение меню и получение выбранного элемента
```csharp
ConsoleSelectorItem consoleSelectorItem = consoleSelector.Show();
```
