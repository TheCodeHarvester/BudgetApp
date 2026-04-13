using System.Collections.ObjectModel;
using System.Windows.Input;
using BudgetApp.Commands.CreditCardAccountsViewCommands;
using BudgetApp.Model;
using BudgetApp.Model.Data;
using BudgetApp.Model.Data.Accounts;
using BudgetApp.Model.Data.NoteScripts;
using BudgetApp.Stores;
using BudgetApp.ViewModels.Rows;

namespace BudgetApp.ViewModels.UserControls;

public class CreditCardsViewModel : ViewModelBase
{
    // Stores
    private readonly PersonStore _personStore;
    private readonly LoginDetailsStore _loginDetailsStore;
    private readonly AccountDetailsStore _accountDetailsStore;
    private readonly CreditCardAccountsStore _creditCardAccountsStore;

    // Lists
    public IEnumerable<Person> People => _personStore.GetPeople();
    private IEnumerable<CreditCardRowViewModel> _creditCardsAccounts;
    public IEnumerable<CreditCardRowViewModel> CreditCardAccounts
    {
        get => _creditCardsAccounts;
        private set
        {
            _creditCardsAccounts = value;
            OnPropertyChanged(nameof(CreditCardAccounts));
        }
    }

    private CreditCardRowViewModel _selectedCreditCard = null!;
    public CreditCardRowViewModel SelectedCreditCard
    {
        get => _selectedCreditCard;
        set
        {
            _creditCardAccountsStore.CreditCardSelected(value.Account, _selectedCreditCard?.Account);

            _selectedCreditCard = value;

            if (_selectedCreditCard != null)
                Task.Run(SetupCreditCardView);

            OnPropertyChanged(nameof(SelectedCreditCard));
        }
    }

    private Interest _selectedInterest = null!;
    public Interest SelectedInterest
    {
        get => _selectedInterest;
        set
        {
            _selectedInterest = value;
            OnPropertyChanged(nameof(SelectedInterest));
        }
    }

    private Note _selectedNote = null!;
    public Note SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            OnPropertyChanged(nameof(SelectedNote));
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

    // Credit Card Commands
    public ICommand AddCreditCardCommand { get; }
    public ICommand RemoveCreditCardCommand { get; }

    // Interests Commands
    public ICommand AddInterestCommand { get; }
    public ICommand RemoveInterestCommand { get; }

    // Note Commands
    public ICommand AddNoteCommand { get; }
    public ICommand RemoveNoteCommand { get; }

    public CreditCardsViewModel(FinanceSystem financeSystem)
    {
        // Grab Store Information
        _personStore = financeSystem.PersonStore;
        _loginDetailsStore = new LoginDetailsStore();
        _accountDetailsStore = new AccountDetailsStore();
        _creditCardAccountsStore = financeSystem.CreditCardAccountsStore;

        // Setup Add Commands
        AddNoteCommand = new AddNoteCommand(this, _creditCardAccountsStore);
        AddInterestCommand = new AddInterestCommand(this, _creditCardAccountsStore);
        AddCreditCardCommand = new AddCreditCardCommand(this, _creditCardAccountsStore, _personStore);

        // Setup Remove Commands
        RemoveNoteCommand = new RemoveNoteCommand(this, _creditCardAccountsStore);
        RemoveInterestCommand = new RemoveInterestCommand(this, _creditCardAccountsStore);
        RemoveCreditCardCommand = new RemoveCreditCardCommand(this, _creditCardAccountsStore);

        // Create Row View Models on Data Grid
        LoadAccounts();
    }

    private async Task SetupCreditCardView()
    {
        LoginDetails = await _loginDetailsStore.InitializeAsync(_selectedCreditCard.Account.LoginDetailsFile);
        AccountDetails = await _accountDetailsStore.InitializeAsync(_selectedCreditCard.Account.AccountDetailsFile);
    }

    public void LoadAccounts()
    {
        CreditCardAccounts = new ObservableCollection<CreditCardRowViewModel>(
            _creditCardAccountsStore.GetCreditCardAccounts()
                .Select(a => new CreditCardRowViewModel(a, _personStore.PeopleLookup())));
    }
}