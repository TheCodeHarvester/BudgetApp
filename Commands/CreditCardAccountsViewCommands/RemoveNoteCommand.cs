using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Stores;
using BudgetApp.ViewModels.UserControls;

namespace BudgetApp.Commands.CreditCardAccountsViewCommands;

public class RemoveNoteCommand : AsyncCommandBase
{
    private readonly CreditCardAccountsStore _creditStore;
    private readonly CreditCardsViewModel _viewModel;

    public RemoveNoteCommand(CreditCardsViewModel viewModel, CreditCardAccountsStore creditStore)
    {
        _viewModel = viewModel;
        _creditStore = creditStore;
        
        _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(CreditCardsViewModel.SelectedCreditCard)
            or nameof(CreditCardsViewModel.SelectedNote))
        {
            OnCanExecuteChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.SelectedCreditCard != null 
               && _viewModel.SelectedNote != null
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _creditStore.RemoveNote(_viewModel.SelectedCreditCard.Account, _viewModel.SelectedNote);
        }
        catch (Exception)
        {
            MessageBox.Show($"Failed to remove note from account", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}