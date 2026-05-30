using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BudgetApp.Core.CustomControls;

public class AddRemoveControl : Control
{
    static AddRemoveControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(AddRemoveControl),
            new FrameworkPropertyMetadata(typeof(AddRemoveControl)));
    }

    // AddCommand
    public static readonly DependencyProperty AddCommandProperty =
        DependencyProperty.Register(
            nameof(AddCommand),
            typeof(ICommand),
            typeof(AddRemoveControl),
            new PropertyMetadata(null));

    public ICommand AddCommand
    {
        get => (ICommand)GetValue(AddCommandProperty);
        set => SetValue(AddCommandProperty, value);
    }

    // RemoveCommand
    public static readonly DependencyProperty RemoveCommandProperty =
        DependencyProperty.Register(
            nameof(RemoveCommand),
            typeof(ICommand),
            typeof(AddRemoveControl),
            new PropertyMetadata(null));

    public ICommand RemoveCommand
    {
        get => (ICommand)GetValue(RemoveCommandProperty);
        set => SetValue(RemoveCommandProperty, value);
    }
}