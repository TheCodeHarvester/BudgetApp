using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using BudgetApp.Model.Data.NoteScripts;

namespace BudgetApp.Model.Data.Accounts;

public class FinancialAccount : BindableBase
{
    protected int _ownerId = 0;
    public int OwnerId
    {
        get => _ownerId;
        set => SetProperty(ref _ownerId, value, nameof(OwnerId));
    }

    protected string _accountName = string.Empty;
    public string AccountName
    {
        get => _accountName;
        set => SetProperty(ref _accountName, value, nameof(AccountName));
    }

    protected string _loginDetailsFile = string.Empty;
    public string LoginDetailsFile
    {
        get => _loginDetailsFile;
        set => SetProperty(ref _loginDetailsFile, value, nameof(LoginDetailsFile));
    }

    protected List<Note> _notes = [];
    public List<Note> Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value, nameof(Notes));
    }
}