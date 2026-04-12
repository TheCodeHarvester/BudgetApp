using System.Text.Json.Serialization;

namespace BudgetApp.Model.Data.Accounts;

public class LoginDetails : BindableBase
{
    private string _username = string.Empty;
    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value, nameof(Username));
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value, nameof(Password));
    }

    private string _websiteURL = string.Empty;
    public string WebsiteURL
    {
        get => _websiteURL;
        set => SetProperty(ref _websiteURL, value, nameof(WebsiteURL));
    }

    public LoginDetails(){ }

    [JsonConstructor]
    public LoginDetails(string username, string password, string websiteUrl)
    {
        _username = username;
        _password = password;
        _websiteURL = websiteUrl;
    }
}