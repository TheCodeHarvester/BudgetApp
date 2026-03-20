using BudgetApp.Model;
using BudgetApp.Model.Data;

namespace BudgetApp.Exceptions;

public class PersonException : Exception
{
    public Person ExistingPerson { get; }
    public Person IncomingPerson { get; }

    public PersonException(Person existingPerson, Person incomingPerson)
    {
        ExistingPerson = existingPerson;
        IncomingPerson = incomingPerson;
    }

    public PersonException(string message, Person existingPerson, Person incomingPerson) : base(message)
    {
        ExistingPerson = existingPerson;
        IncomingPerson = incomingPerson;
    }

    public PersonException(string message, Exception innerException, Person existingPerson, Person incomingPerson) : base(message, innerException)
    {
        ExistingPerson = existingPerson;
        IncomingPerson = incomingPerson;
    }
}