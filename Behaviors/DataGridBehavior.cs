using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BudgetApp.Behaviors;

public class DataGridBehavior
{
    public static readonly DependencyProperty SingleClickEditProperty =
        DependencyProperty.RegisterAttached(
            "SingleClickEdit",
            typeof(bool),
            typeof(DataGridBehavior),
            new PropertyMetadata(false, OnClickEditDataCell));

    public static bool GetSingleClickEdit(DependencyObject obj) => (bool)obj.GetValue(SingleClickEditProperty);
    public static void SetSingleClickEdit(DependencyObject obj, bool value) => obj.SetValue(SingleClickEditProperty, value);

    private static void OnClickEditDataCell(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not DataGrid dataGrid) return;

        if ((bool)e.NewValue)
        {
            dataGrid.GotFocus += DataGrid_GotFocus;
            dataGrid.PreviewMouseLeftButtonDown += DataGrid_PreviewMouseLeftButtonDown;
        }
        else
        {
            dataGrid.GotFocus -= DataGrid_GotFocus;
            dataGrid.PreviewMouseLeftButtonDown -= DataGrid_PreviewMouseLeftButtonDown;
        }
    }

    private static void DataGrid_GotFocus(object sender, RoutedEventArgs e)
    {
        var dep = (DependencyObject)e.OriginalSource;

        if (HandleDataGridFocus(sender, dep))
            e.Handled = true;
    }

    private static void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var dep = (DependencyObject)e.OriginalSource;

        if (HandleDataGridFocus(sender, dep))
            e.Handled = true;
    }

    private static bool HandleDataGridFocus(object sender, DependencyObject dep)
    {
        while (dep != null && dep is not DataGridCell)
            dep = VisualTreeHelper.GetParent(dep);

        if (dep is not DataGridCell cell) return false;
        
        if (cell.IsEditing) return false;

        if (!cell.IsFocused)
            cell.Focus();

        var dataGrid = (DataGrid)sender;

        if (dataGrid.SelectionUnit == DataGridSelectionUnit.FullRow)
        {
            var row = BehaviorUtilities.FindParent<DataGridRow>(cell);
            if (row != null)
                dataGrid.SelectedItem = row.Item;
        }
        else
        {
            dataGrid.CurrentCell = new DataGridCellInfo(cell);
        }

        dataGrid.Dispatcher.InvokeAsync(() =>
        {
            dataGrid.BeginEdit();
            FocusEditingElement(cell);
        }, System.Windows.Threading.DispatcherPriority.Background);

        return true;
    }

    private static void FocusEditingElement(DataGridCell cell)
    {
        cell.Dispatcher.InvokeAsync(() =>
        {
            var textBox = BehaviorUtilities.FindVisualChild<TextBox>(cell);
            if (textBox == null) 
                return;

            textBox.Focus();
            textBox.SelectAll();
        }, System.Windows.Threading.DispatcherPriority.Background);
    }
}