using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Exceptions;
using BudgetApp.Model;
using BudgetApp.Model.Data;
using BudgetApp.Services;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.PeopleViewCommands;

public class EditPersonCommand : AsyncCommandBase
{
    private readonly PersonStore _personStore;
    private readonly PeopleViewModel _viewModel;

    public EditPersonCommand(PeopleViewModel viewModel, PersonStore personStore)
    {
        _personStore = personStore;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.SelectedPerson != null
               && _viewModel.FirstName != string.Empty
               && _viewModel.LastName != string.Empty
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _personStore.UpdatePerson(_viewModel.SelectedPerson,
                new Person(_viewModel.FirstName, _viewModel.LastName));
            _viewModel.FirstName = string.Empty;
            _viewModel.LastName = string.Empty;
            _viewModel.SelectedPerson = null!;
        }
        catch (PersonException ex)
        {
            MessageBox.Show($"Couldn't update: {ex.ExistingPerson.FirstName} {ex.ExistingPerson.LastName}.\n" +
                            $"New name: {ex.IncomingPerson.FirstName} {ex.IncomingPerson.LastName}, is already in list.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show("Error loading database.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(PeopleViewModel.SelectedPerson) 
            or nameof(PeopleViewModel.FirstName)
            or nameof(PeopleViewModel.LastName))
        {
            OnCanExecuteChanged();
        }
    }
}