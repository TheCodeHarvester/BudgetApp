using System.ComponentModel;
using System.Windows;
using BudgetApp.Commands.Base;
using BudgetApp.Model.Data.NoteScripts;
using BudgetApp.Stores;
using BudgetApp.ViewModels.UserControls;

namespace BudgetApp.Commands.CreditCardAccountsViewCommands;

public class AddNoteCommand : AsyncCommandBase
{
    private readonly CreditCardAccountsStore _creditStore;
    private readonly CreditCardsViewModel _viewModel;

    public AddNoteCommand(CreditCardsViewModel viewModel, CreditCardAccountsStore creditStore)
    {
        _viewModel = viewModel;
        _creditStore = creditStore;
        
        _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
    }

    private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(CreditCardsViewModel.SelectedCreditCard))
        {
            OnCanExecuteChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.SelectedCreditCard != null
               && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _creditStore.AddNote(_viewModel.SelectedCreditCard.Account, new Note());
        }
        catch (Exception)
        {
            MessageBox.Show($"Failed to add note to credit card.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}