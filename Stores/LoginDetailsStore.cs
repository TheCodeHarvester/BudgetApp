using System.ComponentModel;
using System.IO;
using BudgetApp.Model.Data.Accounts;
using BudgetApp.Services;

namespace BudgetApp.Stores;

public class LoginDetailsStore : AsyncStoreBase
{
    private const string _folderName = "LoginDetails";
    private string _fileName = string.Empty;
    private LoginDetails LoginDetails = new LoginDetails();

    public async Task<LoginDetails> InitializeAsync(string fileName)
    {
        UnhookItemPropertyChanged(LoginDetails);

        _fileName = fileName;

        var loaded = await FileSystemService.LoadData<LoginDetails>(Path.Combine(_folderName, _fileName));

        if (loaded == null)
            return LoginDetails;

        await ExecuteAsync(() =>
        {
            LoginDetails = loaded;
        });

        HookItemPropertyChanged(LoginDetails);
        return LoginDetails;
    }

    protected override void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ScheduleSave(LoginDetails, Path.Combine(_folderName, _fileName));
    }

    public static async Task<string> CreateFreshLoginFile()
    {
        return await FileSystemService.CreateFileWithGuidName(new LoginDetails(), _folderName);
    }

    public static async Task DeleteFile(string loginDetailsFile)
    {
        await FileSystemService.DeleteFileWithGuidName(Path.Combine(_folderName, loginDetailsFile));
    }
}