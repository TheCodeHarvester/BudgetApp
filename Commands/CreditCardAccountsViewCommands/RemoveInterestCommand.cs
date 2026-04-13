using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Stores;
using BudgetApp.ViewModels.UserControls;

namespace BudgetApp.Commands.CreditCardAccountsViewCommands;

public class RemoveInterestCommand : AsyncCommandBase
{
    private readonly CreditCardAccountsStore _creditStore;
    private readonly CreditCardsViewModel _viewModel;

    public RemoveInterestCommand(CreditCardsViewModel viewModel, CreditCardAccountsStore creditStore)
    {
        _viewModel = viewModel;
        _creditStore = creditStore;
        
        _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(CreditCardsViewModel.SelectedCreditCard)
            or nameof(CreditCardsViewModel.SelectedInterest))
        {
            OnCanExecuteChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.SelectedCreditCard != null 
               && _viewModel.SelectedInterest != null
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _creditStore.RemoveInterest(_viewModel.SelectedCreditCard.Account, _viewModel.SelectedInterest);
        }
        catch (Exception)
        {
            MessageBox.Show($"Failed to remove interest from credit card", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}