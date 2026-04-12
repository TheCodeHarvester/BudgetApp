namespace BudgetApp.Services;

using CredentialManagement;
using System.Security.Cryptography;

public static class CredentialService
{
    private const string Target = "BudgetAppEncryptionKey";

    public static byte[] GetOrCreateKey()
    {
        using var cred = new Credential { Target = Target };

        if (cred.Load())
        {
            return Convert.FromBase64String(cred.Password);
        }

        // Generate new 256-bit key
        var key = RandomNumberGenerator.GetBytes(32);

        cred.Password = Convert.ToBase64String(key);
        cred.Type = CredentialType.Generic;
        cred.PersistanceType = PersistanceType.LocalComputer;

        cred.Save();

        return key;
    }
}