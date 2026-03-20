using BudgetApp.Model.Utility;

namespace BudgetApp.Model.Data.Accounts;

public class SubscriptionAccount : FinancialAccount
{
    public OccurenceType OccurenceType { get; set; } = OccurenceType.Monthly;
}