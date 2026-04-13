using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Model.Data.Accounts;
using BudgetApp.Stores;
using BudgetApp.ViewModels;
using BudgetApp.ViewModels.UserControls;

namespace BudgetApp.Commands.CreditCardAccountsViewCommands;

public class AddCreditCardCommand : AsyncCommandBase
{
    private readonly PersonStore _personStore;
    private readonly CreditCardAccountsStore _creditStore;
    private readonly CreditCardsViewModel _viewModel;

    public AddCreditCardCommand(CreditCardsViewModel viewModel, CreditCardAccountsStore creditStore, 
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
            await _creditStore.AddCreditCard(new CreditCardAccount(accountFile, loginFile));
            _viewModel.LoadAccounts();
        }
        catch (Exception)
        {
            MessageBox.Show($"Failed to add credit card account.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}