using System.Windows;
using BudgetApp.Core.Commands;
using BudgetApp.Core.Stores;
using BudgetApp.Features.CreditCards.Models;
using BudgetApp.Features.CreditCards.Stores;
using BudgetApp.ViewModels;
using BudgetApp.Features.People.Stores;

namespace BudgetApp.Features.CreditCards.Commands;

public class AddCreditCardCommand : AsyncCommandBase
{
    private readonly PersonStore _personStore;
    private readonly CreditCardsStore _creditStore;
    private readonly CreditCardsViewModel _viewModel;

    public AddCreditCardCommand(CreditCardsViewModel viewModel, CreditCardsStore creditStore, PersonStore personStore)
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