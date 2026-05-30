using BudgetApp.Features.CreditCards.Stores;
using BudgetApp.Features.Incomes.Stores;
using BudgetApp.Features.People.Stores;

namespace BudgetApp.Features.Accounts.Stores;

public class FinanceStore
{
    public readonly PersonStore PersonStore;
    public readonly IncomeStore IncomeStore;
    public readonly CreditCardsStore CreditCardsStore;

    public FinanceStore()
    {
        PersonStore = new PersonStore();
        IncomeStore = new IncomeStore();
        CreditCardsStore = new CreditCardsStore();
    }

    public async Task InitializeAsync()
    {
        await PersonStore.InitializeAsync();
        await IncomeStore.InitializeAsync();
        await CreditCardsStore.InitializeAsync();
    }

    public async Task Save()
    {
        await PersonStore.SaveStore();
        await IncomeStore.SaveStore();
        await CreditCardsStore.SaveStore();
    }
}