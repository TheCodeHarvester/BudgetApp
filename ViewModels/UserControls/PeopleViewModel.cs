using System.Windows.Input;
using BudgetApp.Commands.PeopleViewCommands;
using BudgetApp.Model.Data;
using BudgetApp.Stores;

namespace BudgetApp.ViewModels.UserControls;

public class PeopleViewModel : ViewModelBase
{
    private readonly PersonStore _personStore;
    public IEnumerable<Person> People => _personStore.GetPeople();

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

    public ICommand AddPersonCommand { get; }
    public ICommand RemovePersonCommand { get; }

    public PeopleViewModel(PersonStore personStore)
    {
        _personStore = personStore;
        AddPersonCommand = new AddPersonCommand(_personStore);
        RemovePersonCommand = new RemovePersonCommand(this, _personStore);
    }
}