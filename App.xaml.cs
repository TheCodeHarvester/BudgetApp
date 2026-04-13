using System.Windows;
using BudgetApp.Model;
using BudgetApp.Services;
using BudgetApp.Stores;
using BudgetApp.ViewModels;
using BudgetApp.ViewModels.UserControls;

namespace BudgetApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly FinanceSystem _financeSystem = new();
    private readonly NavigationStore _navigationStore = new();

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        await _financeSystem.InitializeAsync();

        _navigationStore.CurrentViewModel = CreatePersonViewModel();

        var navigationServices = new List<NavigationService>
        {
            new(_navigationStore, CreatePersonViewModel),
            new(_navigationStore, CreateIncomesViewModel),
            new(_navigationStore, CreateCreditCardsViewModel),
            new(_navigationStore, CreateLoansViewModel)
        };

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(navigationServices, _navigationStore)
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

    private CreditCardsViewModel CreateCreditCardsViewModel()
    {
        return new CreditCardsViewModel(_financeSystem);
    }

    private LoansViewModel CreateLoansViewModel()
    {
        return new LoansViewModel();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _financeSystem.Save();
        base.OnExit(e);
    }
}