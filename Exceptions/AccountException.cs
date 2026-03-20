using BudgetApp.Model.Data.Accounts;

namespace BudgetApp.Exceptions;

public class AccountException : Exception
{
    public AccountBase Item { get; }

    public AccountException(AccountBase item)
    {
        Item = item;
    }

    public AccountException(string message, AccountBase item) : base(message)
    {
        Item = item;
    }

    public AccountException(string message, Exception innerException, AccountBase item) : base(message, innerException)
    {
        Item = item;
    }
}