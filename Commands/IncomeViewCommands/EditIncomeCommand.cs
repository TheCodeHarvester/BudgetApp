using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.IncomeViewCommands;

public class EditIncomeCommand : AsyncCommandBase
{
    private readonly IncomeStore _incomeStore;
    private readonly IncomesViewModel _viewModel;

    public EditIncomeCommand(IncomesViewModel viewModel, IncomeStore incomeStore)
    {
        _incomeStore = incomeStore;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return !string.IsNullOrEmpty(_viewModel.IncomeSource)
               && !string.IsNullOrEmpty(_viewModel.Amount)
               && _viewModel.SelectedPerson != null
               && _viewModel.OccurenceType != null
               && _viewModel.NextOccurance != DateTime.Now
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _incomeStore.UpdateIncome(_viewModel.SelectedIncome,
                new Income(_viewModel.IncomeSource, 
                    double.Parse(_viewModel.Amount),
                    _viewModel.SelectedPerson.FullName, 
                    _viewModel.OccurenceType,
                    _viewModel.NextOccurance));
            _viewModel.ResetIncomeInfo();
        }
        catch (IncomeException ex)
        {
            MessageBox.Show($"Couldn't update: {ex.ExistingIncome.IncomeSource} {ex.ExistingIncome.Amount}.\n" +
                            $"New name: {ex.IncomingIncome.IncomeSource} {ex.IncomingIncome.Amount}, is already in list.",
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