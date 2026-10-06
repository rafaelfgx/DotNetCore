using System.Security.Cryptography;
using System.Text;

namespace DotNetCore.Security;

public sealed class CryptographyService(string password) : ICryptographyService
{
    private const int KeySize = 32;
    private const int Iterations = 500_000;
    private const int NonceSize = 12;
    private const int TagSize = 16;

    private byte[] DeriveKey(string salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2
        (
            Encoding.UTF8.GetBytes(password),
            Encoding.UTF8.GetBytes(salt),
            Iterations,
            HashAlgorithmName.SHA3_512,
            KeySize
        );
    }

    public string Encrypt(string value, string salt)
    {
        var valueBytes = Encoding.UTF8.GetBytes(value);

        var cipherText = new byte[valueBytes.Length];

        var nonce = RandomNumberGenerator.GetBytes(NonceSize);

        var tag = new byte[TagSize];

        using var aesGcm = new AesGcm(DeriveKey(salt), TagSize);

        aesGcm.Encrypt(nonce, valueBytes, cipherText, tag);

        var encryted = new byte[NonceSize + cipherText.Length + TagSize];

        Buffer.BlockCopy(nonce, 0, encryted, 0, NonceSize);

        Buffer.BlockCopy(cipherText, 0, encryted, NonceSize, cipherText.Length);

        Buffer.BlockCopy(tag, 0, encryted, NonceSize + cipherText.Length, TagSize);

        return Convert.ToBase64String(encryted);
    }

    public string Decrypt(string value, string salt)
    {
        var valueBytes = Convert.FromBase64String(value);

        var nonce = new byte[NonceSize];

        var tag = new byte[TagSize];

        var length = valueBytes.Length - NonceSize - TagSize;

        var cipherText = new byte[length];

        var decrypted = new byte[length];

        Buffer.BlockCopy(valueBytes, 0, nonce, 0, NonceSize);

        Buffer.BlockCopy(valueBytes, NonceSize, cipherText, 0, length);

        Buffer.BlockCopy(valueBytes, NonceSize + length, tag, 0, TagSize);

        using var aesGcm = new AesGcm(DeriveKey(salt), TagSize);

        aesGcm.Decrypt(nonce, cipherText, tag, decrypted);

        return Encoding.UTF8.GetString(decrypted);
    }
}
