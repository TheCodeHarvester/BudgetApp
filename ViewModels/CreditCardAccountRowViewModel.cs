using BudgetApp.Model.Data.Accounts;

namespace BudgetApp.ViewModels;

public class CreditCardAccountRowViewModel : ViewModelBase
{
    private readonly Dictionary<int, string> _peopleDictionary;

    private CreditCardAccount _account;
    public CreditCardAccount Account
    {
        get => _account;
        set
        {
            _account = value;
            OnPropertyChanged(nameof(Account));
        }
    }

    public int OwnerId
    {
        get => Account.OwnerId;
        set
        {
            Account.OwnerId = value;
            OnPropertyChanged(nameof(OwnerId));
            OnPropertyChanged(nameof(OwnerName));
        }
    }

    public string AccountName
    {
        get => Account.AccountName;
        set
        {
            Account.AccountName = value;
            OnPropertyChanged(nameof(AccountName));
        }
    }

    public string OwnerName => _peopleDictionary[OwnerId];

    public CreditCardAccountRowViewModel(CreditCardAccount account, Dictionary<int, string> peopleDictionary)
    {
        Account = account;
        _peopleDictionary = peopleDictionary;
    }
}