using BudgetApp.Core.Utilities;
using BudgetApp.Features.Accounts.Models;

namespace BudgetApp.Features.Subscriptions.Models;

public class Subscription : FinancialAccount
{
    public OccurenceType OccurenceType { get; set; } = OccurenceType.Monthly;
}