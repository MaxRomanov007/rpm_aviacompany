using System.Diagnostics.CodeAnalysis;
using System.Linq;
using App.Domain.Static;
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
        var offices = OfficeStorage.GetOffices();

        OfficesComboBox.ItemsSource = offices.Prepend(new Office
        {
            Title = "All offices",
        });
        OfficesComboBox.SelectedIndex = 0;

        UsersDataGrid.ItemsSource = UserStorage.GetUsersByOfficeId(0);
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private void OfficesComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox { SelectedItem: Office office })
        {
            return;
        }

        UsersDataGrid.ItemsSource = UserStorage.GetUsersByOfficeId(office.Id);
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

        if (result)
        {
            UsersDataGrid.ItemsSource = UserStorage.GetUsersByOfficeId(office.Id);
        }
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private void ExitButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MainContent.NavigateTo(new AuthorizationPage());
    }
}