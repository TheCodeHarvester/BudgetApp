namespace BudgetApp.Model.Data.Accounts;

public class CreditCard : TransactionAccount
{
    public string CreditCardNumber { get; set; } = "";
    public List<Interest> Interests { get; set; } = [];
}