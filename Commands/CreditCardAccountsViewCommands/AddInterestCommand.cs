using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Model.Data;
using BudgetApp.Stores;
using BudgetApp.ViewModels.UserControls;

namespace BudgetApp.Commands.CreditCardAccountsViewCommands;

public class AddInterestCommand : AsyncCommandBase
{
    private readonly CreditCardAccountsStore _creditStore;
    private readonly CreditCardsViewModel _viewModel;

    public AddInterestCommand(CreditCardsViewModel viewModel, CreditCardAccountsStore creditStore)
    {
        _viewModel = viewModel;
        _creditStore = creditStore;
        
        _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(CreditCardsViewModel.SelectedCreditCard))
        {
            OnCanExecuteChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.SelectedCreditCard != null
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _creditStore.AddInterst(_viewModel.SelectedCreditCard.Account, new Interest());
        }
        catch (Exception)
        {
            MessageBox.Show($"Failed to add interest to credit card.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}