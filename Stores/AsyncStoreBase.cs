using BudgetApp.Services;

namespace BudgetApp.Stores;

public class AsyncStoreBase
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private CancellationTokenSource? _saveCts;
    private readonly TimeSpan _saveDelay = TimeSpan.FromSeconds(1);

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

    protected async Task<T> ExecuteAsync<T>(Func<T> func)
    {
        await _semaphore.WaitAsync();
        try
        {
            return func();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    protected async Task ExecuteAsync(Func<Task> func)
    {
        await _semaphore.WaitAsync();
        try
        {
            await func();
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
}