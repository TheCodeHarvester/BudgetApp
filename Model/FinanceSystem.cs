using BudgetApp.Stores;

namespace BudgetApp.Model;

public class FinanceSystem
{
    public readonly PersonStore PersonStore;

    public FinanceSystem()
    {
        PersonStore = new PersonStore();
    }

    public async Task InitializeAsync()
    {
        await PersonStore.InitializeAsync();
    }

    public async Task Save()
    {
        await PersonStore.SaveStore();
    }
}