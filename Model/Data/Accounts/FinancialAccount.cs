namespace BudgetApp.Model.Data.Accounts;

public class FinancialAccount : AccountBase
{
    public DateTime Date { get; set; } = DateTime.Today;
    public double Amount { get; set; } = 0.0;
}