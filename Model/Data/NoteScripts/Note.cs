using System.Text.Json.Serialization;

namespace BudgetApp.Model.Data.NoteScripts;

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