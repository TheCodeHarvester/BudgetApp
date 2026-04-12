using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Exceptions;
using BudgetApp.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.IncomeViewCommands;

public class RemoveIncomeCommand : AsyncCommandBase
{
    private readonly IncomeStore _incomeStore;
    private readonly IncomesViewModel _viewModel;

    public RemoveIncomeCommand(IncomesViewModel viewModel, IncomeStore incomeStore)
    {
        _incomeStore = incomeStore;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.SelectedIncome != null
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _incomeStore.RemoveIncome(_viewModel.SelectedIncome.Account);
            _viewModel.LoadIncomes();
        }
        catch (IncomeException ex)
        {
            MessageBox.Show($"{ex.IncomingIncome.IncomeSource} {ex.ExistingIncome.IncomeSource}, Not in list.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show("Income not in list.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(IncomesViewModel.SelectedIncome))
        {
            OnCanExecuteChanged();
        }
    }
}