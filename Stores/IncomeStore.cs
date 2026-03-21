using System.Collections.ObjectModel;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data;
using BudgetApp.Services;

namespace BudgetApp.Stores;

public class IncomeStore : AsyncStoreBase
{
    private const string _fileName = "Incomes.Json";
    private ObservableCollection<Income> _incomes = [];
    
    public async Task InitializeAsync()
    {
        var loaded = await FileSystemService.LoadData<List<Income>>(_fileName);

        if (loaded == null)
            return;

        await ExecuteAsync(() =>
        {
            _incomes = new ObservableCollection<Income>(loaded);
        });
    }

    public ObservableCollection<Income> GetIncomes()
    {
        return _incomes;
    }

    public Task AddIncome(Income income)
    {
        return ExecuteAsync(() =>
        {
            var conflictingIncome = CheckForConflictingIncome(income);

            if (conflictingIncome != null)
                throw new IncomeException(conflictingIncome, income);

            _incomes.Add(income);
            ScheduleSave(_incomes, _fileName);
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
            ScheduleSave(_incomes, _fileName);
        });
    }

    public Task UpdateIncome(Income existingIncome, Income incomingIncome)
    {
        return ExecuteAsync(() => {
            var personToEdit = CheckForConflictingIncome(existingIncome);

            if (personToEdit == null)
                throw new IncomeException(existingIncome, incomingIncome);

            personToEdit.EditIncome(incomingIncome);
            ScheduleSave(_incomes, _fileName);
        });
    }

    private Income? CheckForConflictingIncome(Income incomingPerson)
    {
        return _incomes.FirstOrDefault(x => x.Name == incomingPerson.Name
                                            && x.IncomeName == incomingPerson.IncomeName
                                            && Math.Abs(x.Amount - incomingPerson.Amount) < 0.1
                                            && x.LastOccurence == incomingPerson.LastOccurence
                                            && x.OccurenceType == incomingPerson.OccurenceType) ?? null;
    }

    public async Task SaveStore()
    {
        await Save(_incomes, _fileName);
    }
}