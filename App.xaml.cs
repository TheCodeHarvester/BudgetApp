using System.Windows;
using BudgetApp.Core.Services;
using BudgetApp.Core.Stores;
using BudgetApp.Core.ViewModels;
using BudgetApp.Features.Accounts.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly FinanceStore _financeStore = new();

    protected override async void OnStartup(StartupEventArgs e)
    {
        AppContext.SetSwitch("Switch.System.Windows.Controls.Text.UseAdornerForTextboxSelectionRendering", false);
        base.OnStartup(e);

        await _financeStore.InitializeAsync();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_financeStore)
        };

        MainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _financeStore.Save();
        base.OnExit(e);
    }
}