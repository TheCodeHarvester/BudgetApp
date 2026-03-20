namespace BudgetApp.Model.Data;

public class Person(string firstName, string lastName) : BindableBase
{
    private string _firstName = firstName;
    public string FirstName
    {
        get => _firstName;
        private set => SetProperty(ref _firstName, value, nameof(FullName));
    }

    private string _lastName = lastName;
    public string LastName
    {
        get => _lastName;
        private set => SetProperty(ref _lastName, value, nameof(FullName));
    }

    public string FullName => $"{FirstName} {LastName}";
    public void EditName(Person newName)
    {
        FirstName = newName.FirstName;
        LastName = newName.LastName;
    }
}