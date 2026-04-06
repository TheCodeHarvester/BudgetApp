using System.Windows.Input;
using BudgetApp.Commands.IncomeViewCommands;
using BudgetApp.Model;
using BudgetApp.Model.Data;
using BudgetApp.Model.Utility;
using BudgetApp.Stores;

namespace BudgetApp.ViewModels;

public class IncomesViewModel : ViewModelBase
{
    private readonly PersonStore _personStore;
    public IEnumerable<Person> People => _personStore.GetPeople();

    private readonly IncomeStore _incomeStore;
    public IEnumerable<Income> Incomes => _incomeStore.GetIncomes();
    public Array OccurenceTypes => Enum.GetValues(typeof(OccurenceType));

    private string _incomeSource;
    public string IncomeSource
    {
        get => _incomeSource;
        set
        {
            _incomeSource = value;
            OnPropertyChanged(nameof(IncomeSource));
        }
    }

    private string _amount;
    public string Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            OnPropertyChanged(nameof(Amount));
        }
    }

    private Person _selectedPerson = null!;
    public Person SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            _selectedPerson = value;
            OnPropertyChanged(nameof(SelectedPerson));
        }
    }

    private OccurenceType _occurenceType;
    public OccurenceType OccurenceType
    {
        get => _occurenceType;
        set
        {
            _occurenceType = value;
            OnPropertyChanged(nameof(OccurenceType));
        }
    }

    private DateTime _nextOccurance = DateTime.Today;
    public DateTime NextOccurance
    {
        get => _nextOccurance;
        set
        {
            _nextOccurance = value;
            OnPropertyChanged(nameof(NextOccurance));
        }
    }

    private Income _selectedIncome = null!;
    public Income SelectedIncome
    {
        get => _selectedIncome;
        set
        {
            _selectedIncome = value;

            if (_selectedIncome != null)
            {
                IncomeSource = _selectedIncome.IncomeSource;
                Amount = _selectedIncome.Amount.ToString("F2");
                SelectedPerson = People.FirstOrDefault(p => p.GrabPerson(_selectedIncome.WhosIncome) != null) ?? SelectedPerson;
                OccurenceType = _selectedIncome.OccurenceType;
                NextOccurance = _selectedIncome.NextOccurance;
                FocusTarget = "IncomeSource";
            }

            OnPropertyChanged(nameof(SelectedIncome));
        }
    }

    private string? _focusTarget;
    public string? FocusTarget
    {
        get => _focusTarget;
        set
        {
            _focusTarget = value;
            OnPropertyChanged(nameof(FocusTarget));
        }
    }

    public ICommand AddIncomeCommand { get; }
    public ICommand EditIncomeCommand { get; }
    public ICommand RemoveIncomeCommand { get; }
    public ICommand CancelIncomeCommand { get; }

    public IncomesViewModel(FinanceSystem financeSystem)
    {
        _personStore = financeSystem.PersonStore;
        _incomeStore = financeSystem.IncomeStore;
        AddIncomeCommand = new AddIncomeCommand(this, _incomeStore);
        EditIncomeCommand = new EditIncomeCommand(this, _incomeStore);
        RemoveIncomeCommand = new RemoveIncomeCommand(this, _incomeStore);
        CancelIncomeCommand = new CancelIncomeCommand(this);
    }

    public void ResetIncomeInfo()
    {
        IncomeSource = string.Empty;
        Amount = string.Empty;
        SelectedPerson = null!;
        SelectedIncome = null!;
        OccurenceType = OccurenceType.NotSet;
    }
}