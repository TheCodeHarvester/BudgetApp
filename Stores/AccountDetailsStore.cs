using System.ComponentModel;
using System.IO;
using BudgetApp.Model.Data.Accounts;
using BudgetApp.Services;

namespace BudgetApp.Stores;

public class AccountDetailsStore : AsyncStoreBase
{
    private const string _folderName = "AccountDetails";
    private string _fileName = string.Empty;
    private AccountDetails AccountDetails = new AccountDetails();

    public async Task<AccountDetails> InitializeAsync(string fileName)
    {
        UnhookItemPropertyChanged(AccountDetails);

        _fileName = fileName;

        var loaded = await FileSystemService.LoadData<AccountDetails>(Path.Combine(_folderName, _fileName));

        if (loaded == null)
            return AccountDetails;

        await ExecuteAsync(() =>
        {
            AccountDetails = loaded;
        });

        HookItemPropertyChanged(AccountDetails);
        return AccountDetails;
    }

    protected override void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ScheduleSave(AccountDetails, Path.Combine(_folderName, _fileName));
    }

    public static async Task<string> CreateFreshAccountFile()
    {
        return await FileSystemService.CreateFileWithGuidName(new AccountDetails(), _folderName);
    }

    public static async Task DeleteFile(string accountDetailsFile)
    {
        await FileSystemService.DeleteFileWithGuidName(Path.Combine(_folderName, accountDetailsFile));
    }
}