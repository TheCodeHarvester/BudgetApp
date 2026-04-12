using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Exceptions;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.PeopleViewCommands;

public class RemovePersonCommand : AsyncCommandBase
{
    private readonly PersonStore _personStore;
    private readonly PeopleViewModel _viewModel;

    public RemovePersonCommand(PeopleViewModel viewModel, PersonStore personStore)
    {
        _personStore = personStore;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.SelectedPerson != null
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _personStore.RemovePerson(_viewModel.SelectedPerson);
        }
        catch (PersonException ex)
        {
            MessageBox.Show($"{ex.ExistingPerson.FirstName} {ex.ExistingPerson.LastName}, Not in list.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show("Person not in list.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(PeopleViewModel.SelectedPerson))
        {
            OnCanExecuteChanged();
        }
    }
}