using BudgetApp.Stores;

namespace BudgetApp.Model;

public class FinanceSystem
{
    public readonly PersonStore PersonStore;
    public readonly IncomeStore IncomeStore;

    public FinanceSystem()
    {
        PersonStore = new PersonStore();
        IncomeStore = new IncomeStore();
    }

    public async Task InitializeAsync()
    {
        await PersonStore.InitializeAsync();
        await IncomeStore.InitializeAsync();
    }

    public async Task Save()
    {
        await PersonStore.SaveStore();
        await IncomeStore.SaveStore();
    }
}