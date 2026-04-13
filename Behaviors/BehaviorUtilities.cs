using System.Windows;
using System.Windows.Media;

namespace BudgetApp.Behaviors;

public static class BehaviorUtilities
{
    public static T? FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
    {
        for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);

            if (child is T correctlyTyped)
                return correctlyTyped;

            var descendent = FindVisualChild<T>(child);
            if (descendent != null)
                return descendent;
        }

        return null;
    }

    public static T? FindParent<T>(DependencyObject child) where T : DependencyObject
    {
        var parent = VisualTreeHelper.GetParent(child);

        while (parent != null && parent is not T)
        {
            parent = VisualTreeHelper.GetParent(parent);
        }

        return parent as T;
    }
}