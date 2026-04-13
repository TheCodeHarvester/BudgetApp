using System.Collections.ObjectModel;
using System.Windows.Input;
using BudgetApp.Commands.IncomeViewCommands;
using BudgetApp.Model;
using BudgetApp.Model.Data;
using BudgetApp.Model.Utility;
using BudgetApp.Stores;
using BudgetApp.ViewModels.Rows;

namespace BudgetApp.ViewModels.UserControls;

public class IncomesViewModel : ViewModelBase
{
    // Stores
    private readonly PersonStore _personStore;
    private readonly IncomeStore _incomeStore;

    // Converter
    public Array OccurenceTypes => Enum.GetValues(typeof(OccurenceType));

    // Lists
    public IEnumerable<Person> People => _personStore.GetPeople();
    private IEnumerable<IncomeRowViewModel> _incomes;

    public IEnumerable<IncomeRowViewModel> Incomes
    {
        get => _incomes;
        private set
        {
            _incomes = value;
            OnPropertyChanged(nameof(Incomes));
        }
    }

    private IncomeRowViewModel _selectedIncome = null!;
    public IncomeRowViewModel SelectedIncome
    {
        get => _selectedIncome;
        set
        {
            _selectedIncome = value;
            OnPropertyChanged(nameof(SelectedIncome));
        }
    }

    public ICommand AddIncomeCommand { get; }
    public ICommand RemoveIncomeCommand { get; }

    public IncomesViewModel(FinanceSystem financeSystem)
    {
        _personStore = financeSystem.PersonStore;
        _incomeStore = financeSystem.IncomeStore;
        AddIncomeCommand = new AddIncomeCommand(this, _incomeStore);
        RemoveIncomeCommand = new RemoveIncomeCommand(this, _incomeStore);
        LoadIncomes();
    }

    public void LoadIncomes()
    {
        Incomes = new ObservableCollection<IncomeRowViewModel>(_incomeStore.GetIncomes()
                .Select(a => new IncomeRowViewModel(a, _personStore.PeopleLookup())));
    }
}