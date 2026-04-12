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
    private readonly IncomeStore _incomeStore;
    private readonly IncomesViewModel _viewModel;

    public AddIncomeCommand(IncomesViewModel viewModel, IncomeStore incomeStore)
    {
        _incomeStore = incomeStore;
        _viewModel = viewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _incomeStore.AddIncome(new Income());
            _viewModel.LoadIncomes();
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
}