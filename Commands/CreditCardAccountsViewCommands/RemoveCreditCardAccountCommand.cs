using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Exceptions;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.CreditCardAccountsViewCommands;

public class RemoveCreditCardAccountCommand : AsyncCommandBase
{
    private readonly CreditCardAccountsStore _creditCardAccountsStore;
    private readonly CreditCardsAccountsViewModel _viewModel;

    public RemoveCreditCardAccountCommand(CreditCardsAccountsViewModel viewModel, CreditCardAccountsStore creditCardAccountsStore)
    {
        _creditCardAccountsStore = creditCardAccountsStore;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.SelectedCreditCardAccount != null
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            var accountDetailsFile = _viewModel.SelectedCreditCardAccount.Account.AccountDetailsFile;
            var loginDetailsFile = _viewModel.SelectedCreditCardAccount.Account.LoginDetailsFile;
            await _creditCardAccountsStore.RemoveCreditCardAccount(_viewModel.SelectedCreditCardAccount.Account);
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
        if (e.PropertyName is nameof(CreditCardsAccountsViewModel.SelectedCreditCardAccount))
        {
            OnCanExecuteChanged();
        }
    }
}