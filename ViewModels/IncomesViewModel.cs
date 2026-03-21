using BudgetApp.Model;
using BudgetApp.Model.Data;
using BudgetApp.Stores;

namespace BudgetApp.ViewModels;

public class IncomesViewModel : ViewModelBase
{
    private readonly PersonStore _personStore;
    public IEnumerable<Person> People => _personStore.GetPeople();

    private readonly IncomeStore _incomeStore;
    public IEnumerable<Income> Incomes => _incomeStore.GetIncomes();

    private string _place;
    public string Place
    {
        get => _place;
        set
        {
            _place = value;
            OnPropertyChanged(nameof(Place));
        }
    }

    private string _amount;
    public string Amount
    {
        get => _amount;
        set
        {
            Amount = value;
            OnPropertyChanged(nameof(Amount));
        }
    }

    public IncomesViewModel(FinanceSystem financeSystem)
    {
        _personStore = financeSystem.PersonStore;
        _incomeStore = financeSystem.IncomeStore;
    }
}