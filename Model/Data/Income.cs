using BudgetApp.Model.Utility;

namespace BudgetApp.Model.Data;

public class Income(string name, string incomeName, double amount, DateTime date, int category)
{
    public string Name = name;
    public string IncomeName = incomeName;
    public double Amount = amount;
    public DateTime LastOccurence = date;
    public OccurenceType OccurenceType = (OccurenceType)category;
}