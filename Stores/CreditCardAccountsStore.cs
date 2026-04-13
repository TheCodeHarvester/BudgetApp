using System.Collections.ObjectModel;
using System.ComponentModel;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data;
using BudgetApp.Model.Data.Accounts;
using BudgetApp.Model.Data.NoteScripts;
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

    public void CreditCardSelected(CreditCardAccount? newCreditCard, CreditCardAccount? oldCreditCard = null)
    {
        if (oldCreditCard != null)
        {
            UnhookCollection(oldCreditCard.Notes);
            UnhookCollection(oldCreditCard.Interests);
        }

        if (newCreditCard == null) return;

        HookCollection(newCreditCard.Notes);
        HookCollection(newCreditCard.Interests);
    }

#region CreditCard List Functions
    public Task AddCreditCard(CreditCardAccount creditCardAccount)
    {
        return ExecuteAsync(() =>
        {
            var conflictingCreditCardAccount = CheckForConflictingCreditCardAccount(creditCardAccount);

            if (conflictingCreditCardAccount != null)
                throw new CreditCardException(conflictingCreditCardAccount, creditCardAccount);

            _creditCardAccounts.Add(creditCardAccount);
        });
    }

    public Task RemoveCreditCard(CreditCardAccount creditCardAccount)
    {
        return ExecuteAsync(() =>
        {
            var creditCardToRemove = CheckForConflictingCreditCardAccount(creditCardAccount);

            if (creditCardToRemove == null)
                throw new CreditCardException(creditCardAccount, creditCardAccount);

            _creditCardAccounts.Remove(creditCardToRemove);
        });
    }
#endregion

#region CreditCard Interests List Functions
    public Task AddInterst(CreditCardAccount creditCardAccount, Interest interest)
    {
        return ExecuteAsync(() =>
        {
            var conflictingCreditCardAccount = CheckForConflictingCreditCardAccount(creditCardAccount);

            if (conflictingCreditCardAccount == null)
                throw new CreditCardException(creditCardAccount, creditCardAccount);

            conflictingCreditCardAccount.Interests.Add(interest);
        });
    }

    public Task RemoveInterest(CreditCardAccount creditCardAccount, Interest interest)
    {
        return ExecuteAsync(() =>
        {
            var creditCardToRemove = CheckForConflictingCreditCardAccount(creditCardAccount);

            if (creditCardToRemove == null)
                throw new CreditCardException(creditCardAccount, creditCardAccount);

            creditCardToRemove.Interests.Remove(interest);
        });
    }
#endregion

#region CreditCard Interests List Functions
    public Task AddNote(CreditCardAccount creditCardAccount, Note note)
    {
        return ExecuteAsync(() =>
        {
            var conflictingCreditCardAccount = CheckForConflictingCreditCardAccount(creditCardAccount);

            if (conflictingCreditCardAccount == null)
                throw new CreditCardException(creditCardAccount, creditCardAccount);

            conflictingCreditCardAccount.Notes.Add(note);
        });
    }

    public Task RemoveNote(CreditCardAccount creditCardAccount, Note note)
    {
        return ExecuteAsync(() =>
        {
            var creditCardToRemove = CheckForConflictingCreditCardAccount(creditCardAccount);

            if (creditCardToRemove == null)
                throw new CreditCardException(creditCardAccount, creditCardAccount);

            creditCardToRemove.Notes.Remove(note);
        });
    }
#endregion

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