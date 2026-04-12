using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Model.Data.Accounts;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.CreditCardAccountsViewCommands;

public class AddCreditCardAccountCommand : AsyncCommandBase
{
    private readonly PersonStore _personStore;
    private readonly CreditCardAccountsStore _creditStore;
    private readonly CreditCardsAccountsViewModel _viewModel;

    public AddCreditCardAccountCommand(CreditCardsAccountsViewModel viewModel, CreditCardAccountsStore creditStore, 
        PersonStore personStore)
    {
        _viewModel = viewModel;
        _personStore = personStore;
        _creditStore = creditStore;
    }

    public override bool CanExecute(object? parameter)
    {
        return _personStore.GetPeople().Count > 0
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            var accountFile = await AccountDetailsStore.CreateFreshAccountFile();
            var loginFile = await LoginDetailsStore.CreateFreshLoginFile();
            await _creditStore.AddCreditCardAccount(new CreditCardAccount(accountFile, loginFile));
            _viewModel.LoadAccounts();
        }
        catch (Exception)
        {
            MessageBox.Show($"Failed to add credit card account.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}