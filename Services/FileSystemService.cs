using System.IO;
using System.Text.Json;

namespace BudgetApp.Services;

public static class FileSystemService
{
    private static readonly string _LocalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private static readonly string _LocalFilePath = Path.Combine(_LocalDirectory, "BudgetApp");

    public static async Task SaveData<T>(T data, string fileLocation)
    {
        var json = JsonSerializer.Serialize(data);

        var fullPath = Path.Combine(_LocalFilePath, fileLocation);
        var directory = Path.GetDirectoryName(fullPath);

        if (!string.IsNullOrWhiteSpace(directory))
            Directory.CreateDirectory(directory);

        await File.WriteAllTextAsync(fullPath, json);
    }

    public static async Task<T?> LoadData<T>(string fileLocation)
    {
        var fullPath = Path.Combine(_LocalFilePath, fileLocation);

        if(!File.Exists(fullPath))
            return default(T);

        var json = await File.ReadAllTextAsync(fullPath);
        return JsonSerializer.Deserialize<T>(json);
    }
}