using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Static;
using App.Domain.Utils;
using App.Windows;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Database.Database.Models;
using Database.Storage;

namespace App.Pages;

public partial class AdminPage : UserControl
{
    public AdminPage()
    {
        InitializeComponent();

        List<Office> offices;
        try
        {
            offices = OfficeStorage.GetOffices();
        }
        catch (Exception ex)
        {
            Dialogs.ShowErrorAsync($"Неизвестная ошибка: {ex.Message}");
            return;
        }

        OfficesComboBox.ItemsSource = offices.Prepend(new Office
        {
            Title = "All offices",
        });
        OfficesComboBox.SelectedIndex = 0;

        try
        {
            UsersDataGrid.ItemsSource = UserStorage.GetUsersByOfficeId(0);
        }
        catch (Exception ex)
        {
            Dialogs.ShowErrorAsync($"Неизвестная ошибка: {ex.Message}");
            return;
        }
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private async void OfficesComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox { SelectedItem: Office office })
        {
            return;
        }

        try
        {
            UsersDataGrid.ItemsSource = UserStorage.GetUsersByOfficeId(office.Id);
        }
        catch (Exception ex)
        {
            await Dialogs.ShowErrorAsync($"Неизвестная ошибка: {ex.Message}");
            return;
        }
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private async void AddUserButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (OfficesComboBox.SelectedItem is not Office office)
        {
            return;
        }

        var dialog = new AddUserWindow();
        var window = this.FindAncestorOfType<Window>();
        if (window == null) return;

        var result = await dialog.ShowDialog<bool>(window);

        if (!result) return;

        try
        {
            UsersDataGrid.ItemsSource = UserStorage.GetUsersByOfficeId(office.Id);
        }
        catch (Exception ex)
        {
            await Dialogs.ShowErrorAsync($"Неизвестная ошибка: {ex.Message}");
            return;
        }
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private void ExitButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MainContent.NavigateTo(new AuthorizationPage());
    }
}