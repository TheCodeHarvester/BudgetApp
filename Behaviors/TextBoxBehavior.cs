using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BudgetApp.Behaviors;

public static class TextBoxBehavior
{
    // Lose focus on "enter" for Text Box.
    public static readonly DependencyProperty LoseFocusOnEnterProperty =
        DependencyProperty.RegisterAttached(
            "LoseFocusOnEnter",
            typeof(bool),
            typeof(TextBoxBehavior),
            new PropertyMetadata(false, OnLoseFocusOnEnterChanged));

    public static bool GetLoseFocusOnEnter(DependencyObject obj) => (bool)obj.GetValue(LoseFocusOnEnterProperty);
    public static void SetLoseFocusOnEnter(DependencyObject obj, bool value) => obj.SetValue(LoseFocusOnEnterProperty, value);

    private static void OnLoseFocusOnEnterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextBox textBox) return;
        
        if ((bool)e.NewValue)
            textBox.KeyDown += TextBox_KeyDown;
        else
            textBox.KeyDown -= TextBox_KeyDown;
    }

    private static void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && sender is TextBox textBox)
        {
            textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
    
    // Select all in textbox when gaining focus.
    public static readonly DependencyProperty SelectAllOnFocusProperty =
        DependencyProperty.RegisterAttached(
            "SelectAllOnFocus",
            typeof(bool),
            typeof(TextBoxBehavior),
            new PropertyMetadata(false, OnSelectAllOnFocusChanged));

    public static bool GetSelectAllOnFocus(DependencyObject obj) => (bool)obj.GetValue(SelectAllOnFocusProperty);
    public static void SetSelectAllOnFocus(DependencyObject obj, bool value) => obj.SetValue(SelectAllOnFocusProperty, value);

    private static void OnSelectAllOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextBox textBox) return;

        if ((bool)e.NewValue)
        {
            textBox.GotFocus += TextBox_GotFocus;
            textBox.MouseDoubleClick += TextBox_GotFocus;
            textBox.PreviewMouseLeftButtonDown += TextBox_PreviewMouseLeftButtonDown;
        }
        else
        {
            textBox.GotFocus -= TextBox_GotFocus;
            textBox.MouseDoubleClick -= TextBox_GotFocus;
            textBox.PreviewMouseLeftButtonDown -= TextBox_PreviewMouseLeftButtonDown;
        }
    }

    private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            textBox.SelectAll();
        }
    }

    private static void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not TextBox textBox || textBox.IsKeyboardFocusWithin) return;

        textBox.Focus();
        e.Handled = true;
    }
}