using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using BudgetApp.Model.Data.NoteScripts;

namespace BudgetApp.Model.Data.Accounts;

public class CreditCardAccount : FinancialAccount
{
    private readonly string _accountDetailsFile = string.Empty;
    public string AccountDetailsFile
    {
        get => _accountDetailsFile;
        init => SetProperty(ref _accountDetailsFile, value, nameof(AccountDetailsFile));
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

    private readonly ObservableCollection<Interest> _interests = [];
    public ObservableCollection<Interest> Interests
    {
        get => _interests;
        init => SetProperty(ref _interests, value, nameof(Interests));
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
        LoginDetailsFile = loginFile;
        AccountDetailsFile = accountFile;
    }

    [JsonConstructor]
    public CreditCardAccount(string accountDetailsFile, DateTime expirationDate, double creditLine, double balance,
        ObservableCollection<Interest> interests, double minimumDue, string cycleDay, int ownerId, string accountName, 
        string loginDetailsFile, ObservableCollection<Note> notes)
    {
        LoginDetailsFile = loginDetailsFile;
        Notes = notes;
        AccountDetailsFile = accountDetailsFile;
        ExpirationDate = expirationDate;
        CreditLine = creditLine;
        Balance = balance;
        Interests = interests;
        MinimumDue = minimumDue;
        CycleDay = cycleDay;
        OwnerId = ownerId;
        AccountName = accountName;
    }
}