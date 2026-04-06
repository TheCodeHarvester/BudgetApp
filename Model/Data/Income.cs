using System.Text.Json.Serialization;
using BudgetApp.Model.Utility;

namespace BudgetApp.Model.Data;

public class Income : BindableBase
{
    private string _incomeSource;
    public string IncomeSource
    {
        get => _incomeSource;
        private set => SetProperty(ref _incomeSource, value, nameof(IncomeSource));
    }

    private double _amount;
    public double Amount
    {
        get => _amount;
        private set => SetProperty(ref _amount, value, nameof(Amount));
    }

    private string _whosIncome;
    public string WhosIncome
    {
        get => _whosIncome;
        private set => SetProperty(ref _whosIncome, value, nameof(WhosIncome));
    }

    private OccurenceType _occurenceType;
    public OccurenceType OccurenceType
    {
        get => _occurenceType;
        private set => SetProperty(ref _occurenceType, value, nameof(OccurenceType));
    }

    private DateTime _nextOccurance;
    public DateTime NextOccurance
    {
        get => _nextOccurance;
        private set => SetProperty(ref _nextOccurance, value, nameof(NextOccurance));
    }

    [JsonConstructor]
    public Income(string incomeSource, double amount, string whosIncome, OccurenceType occurenceType, DateTime nextOccurance)
    {
        _incomeSource = incomeSource;
        _amount = amount;
        _whosIncome = whosIncome;
        _occurenceType = occurenceType;
        _nextOccurance = nextOccurance;
    }

    public void EditIncome(Income income)
    {
        IncomeSource = income.IncomeSource;
        WhosIncome = income.WhosIncome;
        Amount = income.Amount;
        OccurenceType = income.OccurenceType;
        NextOccurance = income.NextOccurance;
    }
}