using System.Text.Json.Serialization;

namespace BudgetApp.Model.Data;

public class Interest : BindableBase
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value, nameof(Name));
    }

    private float _percentage = 0.0f;
    public float Percentage
    {
        get => _percentage;
        set => SetProperty(ref _percentage, value, nameof(Percentage));
    }

    public Interest(){}

    [JsonConstructor]
    public Interest(string name, float percentage)
    {
        _name = name;
        _percentage = percentage;
    }
}