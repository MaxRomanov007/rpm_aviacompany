using App.Domain.Static;
using App.Pages;
using Avalonia.Controls;

namespace App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContent.Init(MainContentControl);
        MainContent.NavigateTo(new AuthorizationPage());
    }
}