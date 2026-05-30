using BudgetApp.Core.Utilities;
using BudgetApp.Features.Accounts.Models;
using BudgetApp.Features.Incomes.Models;

namespace BudgetApp.Features.Banks.Models;

public class BankAccount : FinancialAccount
{
    public string AccountNumber { get; set; } = string.Empty;
    public string RoutingNumber { get; set; } = string.Empty;
    private List<Income> incomes { get; set; } = [];
    public BankType Type { get; set; } = BankType.NotSet;
}