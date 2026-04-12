using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BudgetApp.Services;

public static class FileSystemService
{
    private static readonly string _LocalDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private static readonly string _LocalFilePath = Path.Combine(_LocalDirectory, "BudgetApp");

    public static async Task SaveData<T>(T data, string fileLocation)
    {
        var fullPath = Path.Combine(_LocalFilePath, fileLocation);
        var directory = Path.GetDirectoryName(fullPath);

        if (!string.IsNullOrWhiteSpace(directory))
            Directory.CreateDirectory(directory);

        var key = CredentialService.GetOrCreateKey();
        var json = JsonSerializer.Serialize(data);
        var encryptedBytes = Encrypt(json, key);

        await File.WriteAllBytesAsync(fullPath, encryptedBytes);
    }

    public static async Task<T?> LoadData<T>(string fileLocation)
    {
        var fullPath = Path.Combine(_LocalFilePath, fileLocation);

        if(!File.Exists(fullPath))
            return default(T);

        var key = CredentialService.GetOrCreateKey();
        var encryptedBytes = await File.ReadAllBytesAsync(fullPath);
        var json = Decrypt(encryptedBytes, key);

        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            }
        );
    }

    private static byte[] Encrypt(string plainText, byte[] key)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        using var ms = new MemoryStream();

        ms.Write(aes.IV, 0, aes.IV.Length);

        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        return ms.ToArray();
    }

    private static string Decrypt(byte[] encryptedData, byte[] key)
    {
        using var aes = Aes.Create();
        aes.Key = key;

        using var ms = new MemoryStream(encryptedData);

        var iv = new byte[16];
        ms.ReadExactly(iv, 0, iv.Length);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }

    public static async Task<string> CreateFileWithGuidName<T>(T data, string directory)
    {
        var fullPath = Path.Combine(_LocalFilePath, directory);

        if (!string.IsNullOrWhiteSpace(fullPath))
            Directory.CreateDirectory(fullPath);

        string filePath;
        string guidFileName;

        do
        {
            guidFileName = $"{Guid.NewGuid()}.json";
            filePath = Path.Combine(fullPath, guidFileName);
        }
        while (File.Exists(filePath));

        await SaveData(data, filePath);

        return guidFileName;
    }

    public static async Task DeleteFileWithGuidName(string fileName)
    {
        await Task.Run(() => File.Delete(Path.Combine(_LocalFilePath, fileName)));
    }
}