using BudgetApp.Model.Data;

namespace BudgetApp.ViewModels.Rows;

public class IncomeRowViewModel : ViewModelBase
{
    private readonly Dictionary<int, string> _peopleDictionary;
    
    private Income _account;
    public Income Account
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

    public string OwnerName => _peopleDictionary[OwnerId];

    public IncomeRowViewModel(Income account, Dictionary<int, string> peopleDictionary)
    {
        Account = account;
        _peopleDictionary = peopleDictionary;
    }
}