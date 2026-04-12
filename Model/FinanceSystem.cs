using BudgetApp.Stores;

namespace BudgetApp.Model;

public class FinanceSystem
{
    public readonly PersonStore PersonStore;
    public readonly IncomeStore IncomeStore;
    public readonly CreditCardAccountsStore CreditCardAccountsStore;

    public FinanceSystem()
    {
        PersonStore = new PersonStore();
        IncomeStore = new IncomeStore();
        CreditCardAccountsStore = new CreditCardAccountsStore();
    }

    public async Task InitializeAsync()
    {
        await PersonStore.InitializeAsync();
        await IncomeStore.InitializeAsync();
        await CreditCardAccountsStore.InitializeAsync();
    }

    public async Task Save()
    {
        await PersonStore.SaveStore();
        await IncomeStore.SaveStore();
        await CreditCardAccountsStore.SaveStore();
    }
}