using System.Collections.ObjectModel;
using System.Windows.Input;
using BudgetApp.Commands.PeopleViewCommands;
using BudgetApp.Model;
using BudgetApp.Model.Data;

namespace BudgetApp.ViewModels;

public class IncomesViewModel : ViewModelBase
{
    private readonly ObservableCollection<string> _people;
    public IEnumerable<string> People => _people;

    //private readonly ObservableCollection<IncomesViewModel> _incomes;
    //public IEnumerable<IncomesViewModel> Incomes => _incomes;

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
        _people = new ObservableCollection<string>();
    }

    public void UpdatePeople(IEnumerable<Person> people)
    {
        _people.Clear();
        foreach (var person in people)
            _people.Add(person.FirstName + " " + person.LastName);
    }
}