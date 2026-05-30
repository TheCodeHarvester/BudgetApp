using System.ComponentModel;
using System.Windows;
using BudgetApp.Core.Commands;
using BudgetApp.Core.Exceptions;
using BudgetApp.Core.Stores;
using BudgetApp.Features.CreditCards.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Features.CreditCards.Commands;

public class RemoveCreditCardCommand : AsyncCommandBase
{
    private readonly CreditCardsStore _creditCardsStore;
    private readonly CreditCardsViewModel _viewModel;

    public RemoveCreditCardCommand(CreditCardsViewModel viewModel, CreditCardsStore creditCardsStore)
    {
        _creditCardsStore = creditCardsStore;
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
            await _creditCardsStore.RemoveCreditCard(_viewModel.SelectedCreditCard.Account);
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