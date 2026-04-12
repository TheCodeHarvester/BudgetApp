using System.ComponentModel;

namespace BudgetApp.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    protected string? _focusTarget;
    public string? FocusTarget
    {
        get => _focusTarget;
        set
        {
            _focusTarget = value;
            OnPropertyChanged(nameof(FocusTarget));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}