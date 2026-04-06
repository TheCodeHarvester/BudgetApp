using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Exceptions;
using BudgetApp.Model.Data;
using BudgetApp.Model.Utility;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.IncomeViewCommands;

public class AddIncomeCommand : AsyncCommandBase
{
    private readonly IncomeStore _personStore;
    private readonly IncomesViewModel _viewModel;

    public AddIncomeCommand(IncomesViewModel viewModel, IncomeStore personStore)
    {
        _personStore = personStore;
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
            await _personStore.AddIncome(new Income(_viewModel.IncomeSource, 
                double.Parse(_viewModel.Amount),
                _viewModel.SelectedPerson.FullName, 
                _viewModel.OccurenceType,
                _viewModel.NextOccurance));

            _viewModel.ResetIncomeInfo();
        }
        catch (IncomeException ex)
        {
            MessageBox.Show($"{ex.IncomingIncome.IncomeSource} {ex.ExistingIncome.IncomeSource}, Already in list.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show($"Error loading database.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(IncomesViewModel.Amount)
            or nameof(IncomesViewModel.OccurenceType)
            or nameof(IncomesViewModel.SelectedIncome))
        {
            OnCanExecuteChanged();
        }
    }
}