using System.Text.Json.Serialization;
using BudgetApp.Model.Data.NoteScripts;

namespace BudgetApp.Model.Data.Accounts;

public class CreditCardAccount : FinancialAccount
{
    private string _accountDetailsFile = string.Empty;
    public string AccountDetailsFile
    {
        get => _accountDetailsFile;
        set => SetProperty(ref _accountDetailsFile, value, nameof(AccountDetailsFile));
    }

    private DateTime _expirationDate = DateTime.Today;
    public DateTime ExpirationDate
    {
        get => _expirationDate;
        set => SetProperty(ref _expirationDate, value, nameof(ExpirationDate));
    }

    private double _creditLine = 0.0;
    public double CreditLine
    {
        get => _creditLine;
        set
        {
            SetProperty(ref _creditLine, value, nameof(CreditLine));
            RaisePropertyChanged(nameof(Available));
        } 
    }

    private double _balance = 0.0;
    public double Balance
    {
        get => _balance;
        set
        {
            SetProperty(ref _balance, value, nameof(Balance));
            RaisePropertyChanged(nameof(Available));
        }
    }

    public double Available => CreditLine - Balance;

    private List<Interest> _interests = [];
    public List<Interest> Interests
    {
        get => _interests;
        set => SetProperty(ref _interests, value, nameof(Interests));
    }

    private double _minimumDue = 0.0;
    public double MinimumDue
    {
        get => _minimumDue;
        set => SetProperty(ref _minimumDue, value, nameof(MinimumDue));
    }

    private string _cycleDay = string.Empty;
    public string CycleDay
    {
        get => _cycleDay;
        set => SetProperty(ref _cycleDay, value, nameof(CycleDay));
    }

    public CreditCardAccount(){ }

    public CreditCardAccount(string accountFile, string loginFile)
    {
        _loginDetailsFile = loginFile;
        _accountDetailsFile = accountFile;
    }

    [JsonConstructor]
    public CreditCardAccount(string accountDetailsFile, DateTime expirationDate, double creditLine, double balance,
        List<Interest> interests, double minimumDue, string cycleDay, int ownerId, string accountName, 
        string loginDetailsFile, List<Note> notes)
    {
        _loginDetailsFile = loginDetailsFile;
        _notes = notes;
        _accountDetailsFile = accountDetailsFile;
        _expirationDate = expirationDate;
        _creditLine = creditLine;
        _balance = balance;
        _interests = interests;
        _minimumDue = minimumDue;
        _cycleDay = cycleDay;
        _ownerId = ownerId;
        _accountName = accountName;
    }
}