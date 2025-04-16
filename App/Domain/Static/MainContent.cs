using Avalonia.Controls;

namespace App.Domain.Static;

public static class MainContent
{
    private static ContentControl _control = new();

    public static void Init(ContentControl control)
    {
        _control = control;
    }

    public static void NavigateTo(UserControl to)
    {
        _control.Content = to;
    }
}