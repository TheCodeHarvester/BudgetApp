using System.Text.Json.Serialization;

namespace BudgetApp.Features.Accounts.Models;

public class Note : BindableBase
{
    private string _description = string.Empty;
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value, nameof(Description));
    }

    public Note(){ }

    [JsonConstructor]
    public Note(string description)
    {
        Description = description;
    }
}