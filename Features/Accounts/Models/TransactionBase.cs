using BudgetApp.Core.Utilities;
using BudgetApp.Features.People.Models;

namespace BudgetApp.Features.Accounts.Models;

public class TransactionBase
{
    public Person Owner { get; set; }               // Owner of the account
    public string Place { get; set; }               // Where transaction occurred
    public double Amount { get; set; }              // Amount charged or paid
    public PaymentState PaymentStatus { get; set; } // Payment state
}