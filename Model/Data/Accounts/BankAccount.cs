using BudgetApp.Model.Utility;

namespace BudgetApp.Model.Data.Accounts;

public class BankAccount : FinancialAccount
{
    public string AccountNumber { get; set; } = string.Empty;
    public string RoutingNumber { get; set; } = string.Empty;
    private List<Income> incomes { get; set; } = [];
    public BankType Type { get; set; } = BankType.NotSet;
}