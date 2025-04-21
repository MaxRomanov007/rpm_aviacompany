using System;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using App.Domain.Static;
using App.Domain.Utils;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Database.Database.Models;
using Database.Services;
using Microsoft.Data.SqlClient;

namespace App.Pages;

public partial class AuthorizationPage : UserControl
{
    public AuthorizationPage()
    {
        InitializeComponent();
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private async void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var email = EmailTextBox.Text ?? string.Empty;
        var password = PasswordTextBox.Text ?? string.Empty;

        LoginButton.IsEnabled = false;

        User user;
        try
        {
            user = await UserService.Login(email, password);
        }
        catch (UnauthorizedAccessException)
        {
            await Dialogs.ShowErrorAsync("Неверный логин или пароль");
            return;
        }
        catch (SecurityException)
        {
            await Dialogs.ShowErrorAsync("Вы заблокированы");
            return;
        }
        catch (SqlException)
        {
            await Dialogs.ShowErrorAsync("Ошибка базы данных");
            return;
        }
        catch (Exception ex)
        {
            await Dialogs.ShowErrorAsync($"Неизвестная ошибка: {ex.Message}");
            return;
        }
        finally
        {
            LoginButton.IsEnabled = true;
        }

        if (user.Role.Title == Role.Administrator)
        {
            MainContent.NavigateTo(new AdminPage());
            return;
        }

        await Dialogs.ShowErrorAsync("Вы не администратор =(");
    }
}