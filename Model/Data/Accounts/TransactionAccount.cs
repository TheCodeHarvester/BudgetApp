namespace BudgetApp.Model.Data.Accounts;

public class TransactionAccount : FinancialAccount
{
    public List<FinancialAccount> ChargedAccounts { get; set; } = [];
}