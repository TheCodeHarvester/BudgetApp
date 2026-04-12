using System.Text.Json.Serialization;

namespace BudgetApp.Model.Data;

public class Interest : BindableBase
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        private set => SetProperty(ref _name, value, nameof(Name));
    }

    private double _percentage = 0.0;
    public double Percentage
    {
        get => _percentage;
        private set => SetProperty(ref _percentage, value, nameof(Percentage));
    }

    public Interest(){}

    [JsonConstructor]
    public Interest(string name, double percentage)
    {
        _name = name;
        _percentage = percentage;
    }
}