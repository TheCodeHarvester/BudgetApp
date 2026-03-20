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
    public ICommand NavigateToAccountsView { get; }

    public MainViewModel(NavigationService CreatePersonViewModel, 
        NavigationService CreateAccountViewModel,
        NavigationService CreateIncomesViewModel, 
        NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;

        NavigateToPeopleView = new NavigateCommand(CreatePersonViewModel);
        NavigateToAccountsView = new NavigateCommand(CreateAccountViewModel);
        NavigateToIncomesView = new NavigateCommand(CreateIncomesViewModel);

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}