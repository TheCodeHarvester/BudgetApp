using BudgetApp.Features.Incomes.Models;

namespace BudgetApp.Core.Exceptions;

public class IncomeException : Exception
{
    public Income ExistingIncome { get; }
    public Income IncomingIncome { get; }

    public IncomeException(Income existingIncome, Income incomingIncome)
    {
        ExistingIncome = existingIncome;
        IncomingIncome = incomingIncome;
    }

    public IncomeException(string message, Income existingIncome, Income incomingIncome) : base(message)
    {
        ExistingIncome = existingIncome;
        IncomingIncome = incomingIncome;
    }

    public IncomeException(string message, Exception innerException, Income existingIncome, Income incomingIncome) : base(message, innerException)
    {
        ExistingIncome = existingIncome;
        IncomingIncome = incomingIncome;
    }
}