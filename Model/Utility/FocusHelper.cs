using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BudgetApp.Model.Utility;

public static class FocusHelper
{
    public static string GetFocusKey(DependencyObject obj)
        => (string)obj.GetValue(FocusKeyProperty);

    public static void SetFocusKey(DependencyObject obj, string value)
        => obj.SetValue(FocusKeyProperty, value);

    public static readonly DependencyProperty FocusKeyProperty =
        DependencyProperty.RegisterAttached(
            "FocusKey",
            typeof(string),
            typeof(FocusHelper),
            new PropertyMetadata(null, OnFocusKeyChanged));

    public static string GetFocusTarget(DependencyObject obj)
        => (string)obj.GetValue(FocusTargetProperty);

    public static void SetFocusTarget(DependencyObject obj, string value)
        => obj.SetValue(FocusTargetProperty, value);

    public static readonly DependencyProperty FocusTargetProperty =
        DependencyProperty.RegisterAttached(
            "FocusTarget",
            typeof(string),
            typeof(FocusHelper),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.Inherits,
                OnFocusTargetChanged));



    private static void OnFocusKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TryFocus(d);
    }

    private static void OnFocusTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TryFocus(d);
    }

    private static void TryFocus(DependencyObject d)
    {
        if (d is not UIElement element) 
            return;

        var key = GetFocusKey(d);
        var target = GetFocusTarget(d);

        if (!string.IsNullOrEmpty(key) && key == target)
        {
            element.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new Action(() =>
            {
                element.Focus();
                Keyboard.Focus(element);
            }));
        }
    }

    public static bool GetMoveCaretToEnd(DependencyObject obj)
        => (bool)obj.GetValue(MoveCaretToEndProperty);

    public static void SetMoveCaretToEnd(DependencyObject obj, bool value)
        => obj.SetValue(MoveCaretToEndProperty, value);

    public static readonly DependencyProperty MoveCaretToEndProperty =
        DependencyProperty.RegisterAttached(
            "MoveCaretToEnd",
            typeof(bool),
            typeof(FocusHelper),
            new PropertyMetadata(false, OnMoveCaretToEndChanged));

    private static void OnMoveCaretToEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextBox textBox && (bool)e.NewValue)
        {
            textBox.GotFocus += (s, _) =>
            {
                textBox.Dispatcher.BeginInvoke(
                    System.Windows.Threading.DispatcherPriority.Input,
                    new Action(() =>
                    {
                        textBox.CaretIndex = textBox.Text?.Length ?? 0;
                    }));
            };
        }
    }
}