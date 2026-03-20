using BudgetApp.Model.Utility;

namespace BudgetApp.Model.Data.Accounts;

public class LoanAccount : TransactionAccount
{
    public double Interest { get; set; } = 0.0;
    public LoanType LoanType { get; set; } = LoanType.PersonalLoan;
}