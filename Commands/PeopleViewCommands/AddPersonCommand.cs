using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.PeopleViewCommands;

public class AddPersonCommand : AsyncCommandBase
{
    private readonly PersonStore _personStore;
    private readonly PeopleViewModel _viewModel;

    public AddPersonCommand(PeopleViewModel viewModel, PersonStore personStore)
    {
        _personStore = personStore;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.SelectedPerson == null
               && !string.IsNullOrEmpty(_viewModel.FirstName) 
               && !string.IsNullOrEmpty(_viewModel.LastName) 
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _personStore.AddPerson(new Person(_viewModel.FirstName, _viewModel.LastName));
            _viewModel.FirstName = string.Empty;
            _viewModel.LastName = string.Empty;
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

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(PeopleViewModel.FirstName)
            or nameof(PeopleViewModel.LastName)
            or nameof(PeopleViewModel.SelectedPerson))
        {
            OnCanExecuteChanged();
        }
    }
}