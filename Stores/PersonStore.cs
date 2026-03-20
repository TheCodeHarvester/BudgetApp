using System.Collections.ObjectModel;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data;
using BudgetApp.Services;

namespace BudgetApp.Stores;

public class PersonStore : AsyncStoreBase
{
    private const string _fileName = "People.Json";
    private ObservableCollection<Person> _people = [];

    public async Task InitializeAsync()
    {
        var loaded = await FileSystemService.LoadData<List<Person>>(_fileName);

        if (loaded == null)
            return;

        await ExecuteAsync(() =>
        {
            _people = new ObservableCollection<Person>(loaded);
        });
    }

    public ObservableCollection<Person> GetPeople()
    {
        return _people;
    }

    public Task AddPerson(Person person)
    {
        return ExecuteAsync(() =>
        {
            var conflictingPerson = CheckForConflictingPerson(person);

            if (conflictingPerson != null)
                throw new PersonException(conflictingPerson, person);

            _people.Add(person);
            ScheduleSave(_people, _fileName);
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
            ScheduleSave(_people, _fileName);
        });
    }

    public Task UpdatePerson(Person existingPerson, Person incomingPerson)
    {
        return ExecuteAsync(() => {
            var personToEdit = CheckForConflictingPerson(existingPerson);

            if (personToEdit == null)
               throw new PersonException(existingPerson, incomingPerson);

            personToEdit.EditName(incomingPerson);
            ScheduleSave(_people, _fileName);
        });
    }

    private Person? CheckForConflictingPerson(Person incomingPerson)
    {
        return _people.FirstOrDefault(x => x.FirstName == incomingPerson.FirstName && x.LastName == incomingPerson.LastName) ?? null;
    }

    public async Task SaveStore()
    {
        await Save(_people, _fileName);
    }
}