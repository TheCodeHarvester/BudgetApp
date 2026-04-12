using System.Text.Json.Serialization;
using BudgetApp.Model.Utility;

namespace BudgetApp.Model.Data;

public class Income : BindableBase
{
    private string _incomeSource;
    public string IncomeSource
    {
        get => _incomeSource;
        set => SetProperty(ref _incomeSource, value, nameof(IncomeSource));
    }

    private double _amount;
    public double Amount
    {
        get => _amount;
        set => SetProperty(ref _amount, value, nameof(Amount));
    }

    private int _ownerId = 0;
    public int OwnerId
    {
        get => _ownerId;
        set => SetProperty(ref _ownerId, value, nameof(OwnerId));
    }

    private OccurenceType _occurenceType;
    public OccurenceType OccurenceType
    {
        get => _occurenceType;
        set => SetProperty(ref _occurenceType, value, nameof(OccurenceType));
    }

    private DateTime _nextOccurrence;
    public DateTime NextOccurrence
    {
        get => _nextOccurrence;
        set => SetProperty(ref _nextOccurrence, value, nameof(NextOccurrence));
    }

    public Income()
    {
        _incomeSource = string.Empty;
        _amount = 0;
        _ownerId = 0;
        _occurenceType = OccurenceType.NotSet;
        _nextOccurrence = DateTime.Now;
    }

    [JsonConstructor]
    public Income(int Id, string incomeSource, double amount, int ownerId, OccurenceType occurenceType, 
        DateTime nextOccurrence) : base(Id)
    {
        _incomeSource = incomeSource;
        _amount = amount;
        _ownerId = ownerId;
        _occurenceType = occurenceType;
        _nextOccurrence = nextOccurrence;
    }
}