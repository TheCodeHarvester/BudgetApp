using System.Windows;
using BudgetApp.Model;
using BudgetApp.Services;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private FinanceSystem _financeSystem;
    private NavigationStore _navigationStore;

    public App()
    {
        _financeSystem = new FinanceSystem();
        _navigationStore = new NavigationStore();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        await _financeSystem.InitializeAsync();

        _navigationStore.CurrentViewModel = CreatePersonViewModel();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(new NavigationService(_navigationStore, CreatePersonViewModel), 
                new NavigationService(_navigationStore, CreateAccountsViewModel),
                new NavigationService(_navigationStore, CreateIncomesViewModel), 
                _navigationStore)
        };

        MainWindow.Show();
    }

    private PeopleViewModel CreatePersonViewModel()
    {
        return new PeopleViewModel(_financeSystem.PersonStore);
    }

    private IncomesViewModel CreateIncomesViewModel()
    {
        return new IncomesViewModel(_financeSystem);
    }

    private AccountsViewModel CreateAccountsViewModel()
    {
        return new AccountsViewModel();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _financeSystem.Save();
        base.OnExit(e);
    }
}