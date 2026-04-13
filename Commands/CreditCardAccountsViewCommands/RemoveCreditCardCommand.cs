using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Exceptions;
using BudgetApp.Stores;
using BudgetApp.ViewModels;
using BudgetApp.ViewModels.UserControls;

namespace BudgetApp.Commands.CreditCardAccountsViewCommands;

public class RemoveCreditCardCommand : AsyncCommandBase
{
    private readonly CreditCardAccountsStore _creditCardAccountsStore;
    private readonly CreditCardsViewModel _viewModel;

    public RemoveCreditCardCommand(CreditCardsViewModel viewModel, CreditCardAccountsStore creditCardAccountsStore)
    {
        _creditCardAccountsStore = creditCardAccountsStore;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.SelectedCreditCard != null
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            var accountDetailsFile = _viewModel.SelectedCreditCard.Account.AccountDetailsFile;
            var loginDetailsFile = _viewModel.SelectedCreditCard.Account.LoginDetailsFile;
            await _creditCardAccountsStore.RemoveCreditCard(_viewModel.SelectedCreditCard.Account);
            await LoginDetailsStore.DeleteFile(loginDetailsFile);
            await AccountDetailsStore.DeleteFile(accountDetailsFile);
            _viewModel.LoadAccounts();
        }
        catch (CreditCardException ex)
        {
            MessageBox.Show($"{ex.IncomingCreditCardAccount.AccountName}, not in list.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show($"Failed to remove credit card account.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(CreditCardsViewModel.SelectedCreditCard))
        {
            OnCanExecuteChanged();
        }
    }
}