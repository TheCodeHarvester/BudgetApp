using BudgetApp.Core.Utilities;
using BudgetApp.Features.Accounts.Models;

namespace BudgetApp.Features.Loans.Models;

public class Loan : FinancialAccount
{
    public double Interest { get; set; } = 0.0;
    public LoanType LoanType { get; set; } = LoanType.PersonalLoan;
}