using System;
using System.Diagnostics.CodeAnalysis;
using App.Domain.Utils;
using App.Domain.Validations;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Database.Storage;
using User = App.Domain.Models.User;

namespace App.Windows;

public partial class AddUserWindow : Window
{
    private readonly User _user = new();

    public AddUserWindow()
    {
        InitializeComponent();

        BirthDayDatePicker.MaxYear = new DateTimeOffset(new DateTime().AddYears(-18));

        _user.Birthdate = new DateTimeOffset(DateTime.Now.AddYears(-20));
        DataContext = _user;

        OfficesComboBox.ItemsSource = OfficeStorage.GetOffices();
        OfficesComboBox.SelectedIndex = 0;
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private async void SaveButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var errors = UserValidation.Validate(_user);
        if (!string.IsNullOrEmpty(errors))
        {
            await Dialogs.ShowErrorAsync(errors, "Ошибка валидации");
            return;
        }

        var user = new Database.Database.Models.User
        {
            Birthdate = (_user.Birthdate ?? new DateTimeOffset()).DateTime,
            Email = _user.Email,
            Password = _user.Password,
            FirstName = _user.FirstName,
            LastName = _user.LastName,
            Office = _user.Office
        };

        try
        {
            await UserStorage.SaveUserAsync(user);
        }
        catch (InvalidOperationException)
        {
            await Dialogs.ShowErrorAsync("Такой пользователь уже существует");
            return;
        }
        catch (Exception ex)
        {
            await Dialogs.ShowErrorAsync($"Неизвестная ошибка: {ex.Message}");
            return;
        }

        Close(true);
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}