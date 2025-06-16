using System.Security.Cryptography;
using System.Text;

public static class PassKeyHelper
{
    public static (string PlainKey, byte[] HashedKey) GenerateAndHashPassKey(int length = 32)
    {
        // Generate a secure random key
        var keyBytes = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(keyBytes);

        var plainKey = Convert.ToBase64String(keyBytes);
        var hashedKey = CreateHash(plainKey);

        return (plainKey, hashedKey);
    }

    public static byte[] CreateHash(string input)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        return sha256.ComputeHash(inputBytes);
    }

    public static bool Verify(string inputPlainKey, byte[] storedHashedKey)
    {
        var hashToCompare = CreateHash(inputPlainKey);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, storedHashedKey);
    }
}
