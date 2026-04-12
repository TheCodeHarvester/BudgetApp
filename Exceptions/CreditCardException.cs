using BudgetApp.Model.Data.Accounts;

namespace BudgetApp.Exceptions;

public class CreditCardException : Exception
{
    public CreditCardAccount ExistingCreditCardAccount { get; }
    public CreditCardAccount IncomingCreditCardAccount { get; }

    public CreditCardException(CreditCardAccount existingCreditCardAccount, CreditCardAccount incomingCreditCardAccount)
    {
        ExistingCreditCardAccount = existingCreditCardAccount;
        IncomingCreditCardAccount = incomingCreditCardAccount;
    }

    public CreditCardException(string message, CreditCardAccount existingCreditCardAccount, CreditCardAccount incomingCreditCardAccount) : base(message)
    {
        ExistingCreditCardAccount = existingCreditCardAccount;
        IncomingCreditCardAccount = incomingCreditCardAccount;
    }

    public CreditCardException(string message, Exception innerException, CreditCardAccount existingCreditCardAccount, CreditCardAccount incomingCreditCardAccount) : base(message, innerException)
    {
        ExistingCreditCardAccount = existingCreditCardAccount;
        IncomingCreditCardAccount = incomingCreditCardAccount;
    }
}