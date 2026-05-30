using System.Text.Json.Serialization;
using BudgetApp.Features.Accounts.Models;

namespace BudgetApp.Features.People.Models;

public class Person : BindableBase
{
    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set
        {
            SetProperty(ref _firstName, value, nameof(FullName));
            RaisePropertyChanged(nameof(FirstName));
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
            RaisePropertyChanged(nameof(LastName));
            RaisePropertyChanged(nameof(FullName));
        }
    }

    public string FullName => $"{FirstName} {LastName}";

    public Person()
    {
        FirstName = "First";
        LastName = "Last";
    }

    [JsonConstructor]
    public Person(int id, string firstName, string lastName) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}