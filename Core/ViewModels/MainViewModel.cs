using System.Windows;
using System.Windows.Input;
using BudgetApp.Core.Commands;
using BudgetApp.Core.Services;
using BudgetApp.Core.Stores;
using BudgetApp.Features.Accounts.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Core.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly FinanceStore _financeStore;
    private readonly NavigationStore _navigationStore = new();
    
    // Commands
    public ICommand NavigateToPeopleView { get; }
    public ICommand NavigateToIncomesView { get; }
    public ICommand NavigateToCreditCardsView { get; }
    public ICommand NavigateToLoansView { get; }
    public ICommand NavigateToSettingsView { get; }

    // View Model
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    public bool PeopleViewNotActive => CurrentViewModel is not PeopleViewModel;
    public bool IncomesViewNotActive => CurrentViewModel is not IncomesViewModel;
    public bool CreditCardsViewNotActive => CurrentViewModel is not CreditCardsViewModel;
    public bool LoansViewNotActive => CurrentViewModel is not LoansViewModel;

    public MainViewModel(FinanceStore financeStore)
    {
        _financeStore = financeStore;
        _navigationStore.CurrentViewModel = CreatePersonViewModel();

        NavigateToPeopleView = new NavigateCommand(new NavigationService(_navigationStore, CreatePersonViewModel));
        NavigateToIncomesView = new NavigateCommand(new NavigationService(_navigationStore, CreateIncomesViewModel));
        NavigateToCreditCardsView = new NavigateCommand(new NavigationService(_navigationStore, CreateCreditCardsViewModel));
        NavigateToLoansView = new NavigateCommand(new NavigationService(_navigationStore, CreateLoansViewModel));
        NavigateToSettingsView = new NavigateCommand(new NavigationService(_navigationStore, CreateSettingsViewModel));

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
        OnPropertyChanged(nameof(PeopleViewNotActive));
        OnPropertyChanged(nameof(IncomesViewNotActive));
        OnPropertyChanged(nameof(CreditCardsViewNotActive));
        OnPropertyChanged(nameof(LoansViewNotActive));
    }

    private PeopleViewModel CreatePersonViewModel()
    {
        return new PeopleViewModel(_financeStore.PersonStore);
    }

    private IncomesViewModel CreateIncomesViewModel()
    {
        return new IncomesViewModel(_financeStore);
    }

    private CreditCardsViewModel CreateCreditCardsViewModel()
    {
        return new CreditCardsViewModel(_financeStore);
    }

    private LoansViewModel CreateLoansViewModel()
    {
        return new LoansViewModel();
    }

    private SettingsViewModel CreateSettingsViewModel()
    {
        return new SettingsViewModel();
    }
}