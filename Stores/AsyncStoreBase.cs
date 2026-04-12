using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using BudgetApp.Services;

namespace BudgetApp.Stores;

public class AsyncStoreBase : IDisposable
{
    private bool _isDisposed;
    private CancellationTokenSource? _saveCts;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly TimeSpan _saveDelay = TimeSpan.FromMilliseconds(300);
    private readonly List<(INotifyCollectionChanged collection, NotifyCollectionChangedEventHandler handler)> _trackedCollections = [];

    protected virtual void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs? e) { }

    protected void HookItemPropertyChanged<T>(T item) where T : INotifyPropertyChanged
    {
        item.PropertyChanged += OnItemPropertyChanged;
    }

    protected void UnhookItemPropertyChanged<T>(T item) where T : INotifyPropertyChanged
    {
        item.PropertyChanged -= OnItemPropertyChanged;
    }

    protected void HookCollection<T>(ObservableCollection<T> collection) where T : INotifyPropertyChanged
    {
        foreach (var item in collection)
            item.PropertyChanged += OnItemPropertyChanged;

        NotifyCollectionChangedEventHandler handler = (s, e) =>
        {
            if (e.NewItems != null)
            {
                foreach (T item in e.NewItems)
                    item.PropertyChanged += OnItemPropertyChanged;
            }

            if (e.OldItems != null)
            {
                foreach (T item in e.OldItems)
                    item.PropertyChanged -= OnItemPropertyChanged;
            }

            OnItemPropertyChanged(null, null);
        };

        collection.CollectionChanged += handler;

        _trackedCollections.Add((collection, handler));
    }
    
    protected void UnhookAllCollections()
    {
        foreach (var (collection, handler) in _trackedCollections)
        {
            collection.CollectionChanged -= handler;
        }

        _trackedCollections.Clear();
    }

    protected async Task ExecuteAsync(Action action)
    {
        await _semaphore.WaitAsync();
        try
        {
            action();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    protected void ScheduleSave<T>(T data, string _fileName)
    {
        _saveCts?.Cancel();
        _saveCts = new CancellationTokenSource();

        var token = _saveCts.Token;

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(_saveDelay, token);
                await Save(data, _fileName);
            }
            catch (TaskCanceledException)
            {
                // Ending task because new edit was made.
            }
        });
    }

    protected static async Task Save<T>(T data, string _fileName)
    {
        await FileSystemService.SaveData(data, _fileName);
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;

        UnhookAllCollections();

        _isDisposed = true;
        GC.SuppressFinalize(this);
    }
}