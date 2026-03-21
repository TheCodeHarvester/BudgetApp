using BudgetApp.Model.Utility;

namespace BudgetApp.Model.Data;

public class Income(string name, string incomeName, double amount, DateTime date, OccurenceType category) : BindableBase
{
    private string _name = name;
    public string Name
    {
        get => _name;
        private set => SetProperty(ref _name, value, nameof(Name));
    }

    private string _incomeName = incomeName;
    public string IncomeName
    {
        get => _incomeName;
        private set => SetProperty(ref _incomeName, value, nameof(IncomeName));
    }

    private double _amount = amount;
    public double Amount
    {
        get => _amount;
        private set => SetProperty(ref _amount, value, nameof(Amount));
    }

    private DateTime _lastOccurence = date;
    public DateTime LastOccurence
    {
        get => _lastOccurence;
        private set => SetProperty(ref _lastOccurence, value, nameof(LastOccurence));
    }

    private OccurenceType _occurenceType = category;
    public OccurenceType OccurenceType
    {
        get => _occurenceType;
        private set => SetProperty(ref _occurenceType, value, nameof(OccurenceType));
    }

    public void EditIncome(Income income)
    {
        Name = income.Name;
        IncomeName = income.IncomeName;
        Amount = income.Amount;
        LastOccurence = income.LastOccurence;
        OccurenceType = income.OccurenceType;
    }
}