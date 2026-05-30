using System.Windows;
using BudgetApp.Core.Commands;
using BudgetApp.Core.Exceptions;
using BudgetApp.Features.Incomes.Models;
using BudgetApp.Features.Incomes.Stores;
using BudgetApp.ViewModels;

namespace BudgetApp.Features.Incomes.Commands;

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