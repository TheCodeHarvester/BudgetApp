using System.Collections.ObjectModel;
using System.ComponentModel;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data;
using BudgetApp.Services;

namespace BudgetApp.Stores;

public class PersonStore : AsyncStoreBase
{
    private const string _fileName = "People.Json";
    private ObservableCollection<Person> _people = [];
    public ObservableCollection<Person> GetPeople() => _people;
    public Dictionary<int, string> PeopleLookup() => _people.ToDictionary(p => p.Id, p => p.FullName);

    public async Task InitializeAsync()
    {
        var loaded = await FileSystemService.LoadData<ObservableCollection<Person>>(_fileName);

        await ExecuteAsync(() =>
        {
            if(loaded != null)
                _people = new ObservableCollection<Person>(loaded);

            HookCollection(_people);
        });
    }

    public Task AddPerson(Person person)
    {
        return ExecuteAsync(() =>
        {
            var conflictingPerson = CheckForConflictingPerson(person);

            if (conflictingPerson != null)
                throw new PersonException(conflictingPerson, person);

            _people.Add(person);
        });
    }

    public Task RemovePerson(Person person)
    {
        return ExecuteAsync(() =>
        {
            var personToRemove = CheckForConflictingPerson(person);

            if (personToRemove == null)
                throw new PersonException(person, person);

            _people.Remove(personToRemove);
        });
    }

    private Person? CheckForConflictingPerson(Person incomingPerson)
    {
        return _people.FirstOrDefault(x => x.Id == incomingPerson.Id) ?? null;
    }

    public async Task SaveStore()
    {
        await Save(_people, _fileName);
        UnhookAllCollections();
    }

    protected override void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs? e)
    {
        ScheduleSave(_people, _fileName);
    }
}