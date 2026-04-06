using System.ComponentModel;
using BudgetApp.Commands.Base;
using BudgetApp.Model.Utility;
using BudgetApp.ViewModels;

namespace BudgetApp.Commands.IncomeViewCommands;

public class CancelIncomeCommand : CommandBase
{
    private readonly IncomesViewModel _viewModel;

    public CancelIncomeCommand(IncomesViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.SelectedIncome != null
               && base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        _viewModel.ResetIncomeInfo();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(IncomesViewModel.SelectedIncome))
        {
            OnCanExecuteChanged();
        }
    }
}