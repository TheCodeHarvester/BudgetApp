using System.Text.Json.Serialization;

namespace BudgetApp.Model.Data.Accounts;

public class AccountDetails : BindableBase
{
    private string _accountNumber = string.Empty;  
    public string AccountNumber
    {
        get => _accountNumber;
        set => SetProperty(ref _accountNumber, value, nameof(AccountNumber));
    }

    private string _routingNumber = string.Empty;
    public string RoutingNumber
    {
        get => _routingNumber;
        set => SetProperty(ref _routingNumber, value, nameof(RoutingNumber));
    }

    private string _ccv = string.Empty;
    public string CCV
    {
        get => _ccv;
        set => SetProperty(ref _ccv, value, nameof(CCV));
    }
    
    public AccountDetails(){ }

    [JsonConstructor]
    public AccountDetails(string accountNumber, string routingNumber, string ccv)
    {
        _accountNumber = accountNumber;
        _routingNumber = routingNumber;
        _ccv = ccv;
    }
}