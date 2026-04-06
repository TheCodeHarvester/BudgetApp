using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace BudgetApp.Model.Utility;

public enum AccountType
{
    NotSet,
    Bank,
    CreditCard,
    Loan,
    Subscription
}

public enum BankType
{
    NotSet,
    Checking,
    Savings
}

public enum LoanType
{
    NotSet,
    PersonalLoan,
    StudentLoan,
    CarLoan,
    MortgageLoan
}

public enum PaymentState
{
    NotSet,
    UnPaid,
    Sent,
    Paid
}

public enum OccurenceType
{
    [Description("Not Set")]
    NotSet,
    [Description("Weekly")]
    Weekly,
    [Description("Bi-Weekly")]
    Biweekly,
    [Description("Monthly")]
    Monthly,
    [Description("Yearly")]
    Yearly
}

public class EnumDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return "";

        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}