using System.Windows.Input;
using BudgetApp.Commands.PeopleViewCommands;
using BudgetApp.Model.Data;
using BudgetApp.Stores;

namespace BudgetApp.ViewModels;

public class PeopleViewModel : ViewModelBase
{
    private readonly PersonStore _personStore;
    public IEnumerable<Person> People => _personStore.GetPeople();

    private string _firstName = string.Empty;
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    private Person _selectedPerson = null!;
    public Person SelectedPerson
    {
        get => _selectedPerson;
        set
        {
            _selectedPerson = value;

            if (_selectedPerson != null)
            {
                FirstName = FirstName == string.Empty ? _selectedPerson.FirstName : FirstName;
                LastName = LastName == string.Empty ?  _selectedPerson.LastName : LastName;
                FocusTarget = "FirstName";
            }

            OnPropertyChanged(nameof(SelectedPerson));
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

    public ICommand AddPersonCommand { get; }
    public ICommand EditPersonCommand { get; }
    public ICommand RemovePersonCommand { get; }
    public ICommand CancelPersonCommand { get; }

    public PeopleViewModel(PersonStore personStore)
    {
        _personStore = personStore;
        AddPersonCommand = new AddPersonCommand(this, _personStore);
        EditPersonCommand = new EditPersonCommand(this, _personStore);
        RemovePersonCommand = new RemovePersonCommand(this, _personStore);
        CancelPersonCommand = new CancelPersonCommand(this);
    }
}