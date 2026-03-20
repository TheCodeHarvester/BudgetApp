using System.ComponentModel;
using BudgetApp.Commands.Base;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.PeopleViewCommands;

public class CancelPersonCommand : CommandBase
{
    private readonly PeopleViewModel _viewModel;

    public CancelPersonCommand(PeopleViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.SelectedPerson != null
               && base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        _viewModel.SelectedPerson = null;
        _viewModel.FirstName = string.Empty;
        _viewModel.LastName = string.Empty;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(PeopleViewModel.SelectedPerson))
        {
            OnCanExecuteChanged();
        }
    }
}