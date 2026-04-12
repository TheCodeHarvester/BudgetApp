using System.Collections.ObjectModel;
using System.ComponentModel;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data;
using BudgetApp.Services;

namespace BudgetApp.Stores;

public class IncomeStore : AsyncStoreBase
{
    private const string _fileName = "Incomes.Json";
    private ObservableCollection<Income> _incomes = [];

    public ObservableCollection<Income> GetIncomes()
    {
        return _incomes;
    }

    public async Task InitializeAsync()
    {
        var loaded = await FileSystemService.LoadData<ObservableCollection<Income>>(_fileName);

        await ExecuteAsync(() =>
        {
            if(loaded != null)
                _incomes = new ObservableCollection<Income>(loaded);

            HookCollection(_incomes);
        });
    }

    public Task AddIncome(Income income)
    {
        return ExecuteAsync(() =>
        {
            var conflictingIncome = CheckForConflictingIncome(income);

            if (conflictingIncome != null)
                throw new IncomeException(conflictingIncome, income);

            _incomes.Add(income);
        });
    }

    public Task RemoveIncome(Income income)
    {
        return ExecuteAsync(() =>
        {
            var incomeToRemove = CheckForConflictingIncome(income);

            if (incomeToRemove == null)
                throw new IncomeException(income, income);

            _incomes.Remove(incomeToRemove);
        });
    }

    private Income? CheckForConflictingIncome(Income incomingPerson)
    {
        return _incomes.FirstOrDefault(x => x.Id == incomingPerson.Id) ?? null;
    }

    public async Task SaveStore()
    {
        await Save(_incomes, _fileName);
        UnhookAllCollections();
    }

    protected override void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs? e)
    {
        ScheduleSave(_incomes, _fileName);
    }
}