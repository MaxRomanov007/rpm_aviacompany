using System.Threading.Tasks;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace App.Domain.Utils;

public static class Dialogs
{
    public static async Task ShowErrorAsync(string message, string title = "Ошибка")
    {
        await MessageBoxManager.GetMessageBoxStandard(
            title,
            message,
            ButtonEnum.Ok,
            Icon.Warning
        ).ShowAsync();
    }
}