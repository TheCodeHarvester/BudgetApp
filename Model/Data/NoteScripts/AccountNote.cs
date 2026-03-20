namespace BudgetApp.Model.Data.NoteScripts;

public class AccountNote : Note
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string WebsiteURL { get; set; } = string.Empty;
}