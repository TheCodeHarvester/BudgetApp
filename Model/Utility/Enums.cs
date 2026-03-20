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
    NotSet,
    Weekly,
    Biweekly,
    Monthly,
    Yearly
}