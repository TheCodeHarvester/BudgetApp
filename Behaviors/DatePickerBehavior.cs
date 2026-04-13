using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BudgetApp.Behaviors;

public class DatePickerBehavior
{
    public static readonly DependencyProperty SelectAllOnFocusProperty =
        DependencyProperty.RegisterAttached(
            "SelectAllOnFocus",
            typeof(bool),
            typeof(DatePickerBehavior),
            new PropertyMetadata(false, OnSelectAllOnFocusChanged));

    public static bool GetSelectAllOnFocus(DependencyObject obj) => (bool)obj.GetValue(SelectAllOnFocusProperty);
    public static void SetSelectAllOnFocus(DependencyObject obj, bool value) => obj.SetValue(SelectAllOnFocusProperty, value);

    private static void OnSelectAllOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not DatePicker datePicker) return;

        if ((bool)e.NewValue)
        {
            datePicker.PreviewMouseLeftButtonDown += DatePicker_PreviewMouseLeftButtonDown;
        }
        else
        {
            datePicker.PreviewMouseLeftButtonDown -= DatePicker_PreviewMouseLeftButtonDown;
        }
    }

    private static void DatePicker_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not DatePicker datePicker || datePicker.IsKeyboardFocusWithin) return;

        if (e.OriginalSource is not DependencyObject source ||
            BehaviorUtilities.FindParent<DatePickerTextBox>(source) == null) return;

        datePicker.Focus();
        e.Handled = true;
    }
}