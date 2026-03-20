using BudgetApp.Model.Utility;

namespace BudgetApp.Model.Data.Accounts;

public class AccountBase
{
    public Person Owner { get; set; } = new Person("", "");
    public string AccountName { get; set; } = "Account Name";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string WebsiteURL { get; set; } = string.Empty;
    public AccountType AccountType { get; set; } = AccountType.NotSet;
}