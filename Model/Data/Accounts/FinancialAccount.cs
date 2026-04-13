using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using BudgetApp.Model.Data.NoteScripts;

namespace BudgetApp.Model.Data.Accounts;

public class FinancialAccount : BindableBase
{
    private int _ownerId = 0;
    public int OwnerId
    {
        get => _ownerId;
        set => SetProperty(ref _ownerId, value, nameof(OwnerId));
    }

    private string _accountName = string.Empty;
    public string AccountName
    {
        get => _accountName;
        set => SetProperty(ref _accountName, value, nameof(AccountName));
    }

    private readonly string _loginDetailsFile = string.Empty;
    public string LoginDetailsFile
    {
        get => _loginDetailsFile;
        init => SetProperty(ref _loginDetailsFile, value, nameof(LoginDetailsFile));
    }

    private ObservableCollection<Note> _notes = [];
    public ObservableCollection<Note> Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value, nameof(Notes));
    }
}