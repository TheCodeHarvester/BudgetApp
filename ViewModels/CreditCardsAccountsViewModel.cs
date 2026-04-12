using BudgetApp.Model;
using BudgetApp.Stores;
using BudgetApp.Model.Data;
using System.Windows.Input;
using BudgetApp.Model.Data.Accounts;
using System.Collections.ObjectModel;
using BudgetApp.Commands.CreditCardAccountsViewCommands;

namespace BudgetApp.ViewModels;

public class CreditCardsAccountsViewModel : ViewModelBase
{
    // Stores
    private readonly PersonStore _personStore;
    private readonly LoginDetailsStore _loginDetailsStore;
    private readonly AccountDetailsStore _accountDetailsStore;
    private readonly CreditCardAccountsStore _creditCardAccountsStore;

    // Lists
    public IEnumerable<Person> People => _personStore.GetPeople();
    private IEnumerable<CreditCardAccountRowViewModel> _creditCardsAccounts;
    public IEnumerable<CreditCardAccountRowViewModel> CreditCardAccounts
    {
        get => _creditCardsAccounts;
        private set
        {
            _creditCardsAccounts = value;
            OnPropertyChanged(nameof(CreditCardAccounts));
        }
    }

    private CreditCardAccountRowViewModel _selectedCreditCardAccount = null!;
    public CreditCardAccountRowViewModel SelectedCreditCardAccount
    {
        get => _selectedCreditCardAccount;
        set
        {
            _selectedCreditCardAccount = value;

            if (_selectedCreditCardAccount != null)
                Task.Run(SetupCreditCardView);

            OnPropertyChanged(nameof(SelectedCreditCardAccount));
        }
    }

    private AccountDetails _accountDetails;
    public AccountDetails AccountDetails
    {
        get => _accountDetails;
        set
        { 
            _accountDetails = value;
            OnPropertyChanged(nameof(AccountDetails));
        }
    }

    private LoginDetails _loginDetails;
    public LoginDetails LoginDetails
    {
        get => _loginDetails;
        set
        {
            _loginDetails = value;
            OnPropertyChanged(nameof(LoginDetails));
        }
    }

    public ICommand AddCreditCardAccountCommand { get; }
    public ICommand RemoveCreditCardAccountCommand { get; }

    public CreditCardsAccountsViewModel(FinanceSystem financeSystem)
    {
        _personStore = financeSystem.PersonStore;
        _loginDetailsStore = new LoginDetailsStore();
        _accountDetailsStore = new AccountDetailsStore();
        _creditCardAccountsStore = financeSystem.CreditCardAccountsStore;
        AddCreditCardAccountCommand = new AddCreditCardAccountCommand(this, _creditCardAccountsStore, _personStore);
        RemoveCreditCardAccountCommand = new RemoveCreditCardAccountCommand(this, _creditCardAccountsStore);
        LoadAccounts();
    }

    private async Task SetupCreditCardView()
    {
        LoginDetails = await _loginDetailsStore.InitializeAsync(_selectedCreditCardAccount.Account.LoginDetailsFile);
        AccountDetails = await _accountDetailsStore.InitializeAsync(_selectedCreditCardAccount.Account.AccountDetailsFile);
    }

    public void LoadAccounts()
    {
        CreditCardAccounts = new ObservableCollection<CreditCardAccountRowViewModel>(
            _creditCardAccountsStore.GetCreditCardAccounts()
                .Select(a => new CreditCardAccountRowViewModel(a, _personStore.PeopleLookup())));
    }
}