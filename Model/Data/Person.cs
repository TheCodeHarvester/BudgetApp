using System.Text.Json.Serialization;

namespace BudgetApp.Model.Data;

public class Person : BindableBase
{
    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set
        {
            SetProperty(ref _firstName, value, nameof(FullName));
            RaisePropertyChanged(nameof(FullName));
        }
    }

    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set
        {
            SetProperty(ref _lastName, value, nameof(FullName));
            RaisePropertyChanged(nameof(FullName));
        }
    }

    public string FullName => $"{FirstName} {LastName}";

    public Person()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    [JsonConstructor]
    public Person(int id, string firstName, string lastName) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}