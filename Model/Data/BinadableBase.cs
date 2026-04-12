using System.ComponentModel;

namespace BudgetApp.Model.Data;

public class BindableBase : INotifyPropertyChanged
{
    private int _Id;

    public int Id
    {
        get => _Id;
        private set => SetProperty(ref _Id, value, nameof(Id));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void SetProperty<T>(ref T field, T value, string propertyName)
    {
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected BindableBase()
    {
        Id = Guid.NewGuid().GetHashCode();
    }

    public BindableBase(int id)
    {
        Id = id;
    }
}