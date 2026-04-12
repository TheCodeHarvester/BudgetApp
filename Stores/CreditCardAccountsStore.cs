using System.Collections.ObjectModel;
using System.ComponentModel;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data.Accounts;
using BudgetApp.Services;

namespace BudgetApp.Stores;

public class CreditCardAccountsStore : AsyncStoreBase
{
    private const string _fileName = "CreditCards.Json";
    private ObservableCollection<CreditCardAccount> _creditCardAccounts = [];

    public ObservableCollection<CreditCardAccount> GetCreditCardAccounts()
    {
        return _creditCardAccounts;
    }

    public async Task InitializeAsync()
    {
        var loaded = await FileSystemService.LoadData<ObservableCollection<CreditCardAccount>>(_fileName);

        await ExecuteAsync(() =>
        {
            if(loaded != null)
                _creditCardAccounts = new ObservableCollection<CreditCardAccount>(loaded);

            HookCollection(_creditCardAccounts);
        });
    }

    public Task AddCreditCardAccount(CreditCardAccount creditCardAccount)
    {
        return ExecuteAsync(() =>
        {
            var conflictingCreditCardAccount = CheckForConflictingCreditCardAccount(creditCardAccount);

            if (conflictingCreditCardAccount != null)
                throw new CreditCardException(conflictingCreditCardAccount, creditCardAccount);

            _creditCardAccounts.Add(creditCardAccount);
        });
    }

    public Task RemoveCreditCardAccount(CreditCardAccount creditCardAccount)
    {
        return ExecuteAsync(() =>
        {
            var creditCardToRemove = CheckForConflictingCreditCardAccount(creditCardAccount);

            if (creditCardToRemove == null)
                throw new CreditCardException(creditCardAccount, creditCardAccount);

            _creditCardAccounts.Remove(creditCardToRemove);
        });
    }

    private CreditCardAccount? CheckForConflictingCreditCardAccount(CreditCardAccount incomingPerson)
    {
        return _creditCardAccounts.FirstOrDefault(x => x.Id == incomingPerson.Id);
    }

    public async Task SaveStore()
    {
        await Save(_creditCardAccounts, _fileName);
        UnhookAllCollections();
    }

    protected override void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs? e)
    {
        ScheduleSave(_creditCardAccounts, _fileName);
    }
}