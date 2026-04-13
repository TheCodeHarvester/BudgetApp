using System.Windows.Input;
using BudgetApp.Commands;
using BudgetApp.Model;
using BudgetApp.Services;
using BudgetApp.Stores;

namespace BudgetApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    public ICommand NavigateToPeopleView { get; }
    public ICommand NavigateToIncomesView { get; }
    public ICommand NavigateToCreditCardsView { get; }
    public ICommand NavigateToLoansView { get; }

    public MainViewModel(List<NavigationService> navigationServices, NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        NavigateToPeopleView = new NavigateCommand(navigationServices[0]);
        NavigateToIncomesView = new NavigateCommand(navigationServices[1]);
        NavigateToCreditCardsView = new NavigateCommand(navigationServices[2]);
        NavigateToLoansView = new NavigateCommand(navigationServices[3]);

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}