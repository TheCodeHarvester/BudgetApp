using System.Windows;
using BudgetApp.Core.Commands;
using BudgetApp.Core.Exceptions;
using BudgetApp.Features.People.Models;
using BudgetApp.Features.People.Stores;

namespace BudgetApp.Features.People.Commands;

public class AddPersonCommand : AsyncCommandBase
{
    private readonly PersonStore _personStore;

    public AddPersonCommand(PersonStore personStore)
    {
        _personStore = personStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _personStore.AddPerson(new Person());
        }
        catch (PersonException ex)
        {
            MessageBox.Show($"{ex.IncomingPerson.FirstName} {ex.IncomingPerson.LastName}, Already in list.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show($"Failed to add person.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}