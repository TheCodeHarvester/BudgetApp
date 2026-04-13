using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace BudgetApp.Views;

public partial class CreditCardsView : UserControl
{
    public CreditCardsView()
    {
        InitializeComponent();
    }

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        var regex = new Regex("[^0-9.]+");
        e.Handled = regex.IsMatch(e.Text);
    }
}