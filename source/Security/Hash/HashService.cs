using System.Security.Cryptography;
using System.Text;

namespace DotNetCore.Security;

public sealed class HashService : IHashService
{
    private const int SaltSize = 32;
    private const int KeySize = 64;
    private const int Iterations = 500_000;

    public static string Salt() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(SaltSize));

    public string Create(string value, string salt) => Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2
    (
        Encoding.UTF8.GetBytes(value),
        Encoding.UTF8.GetBytes(salt),
        Iterations,
        HashAlgorithmName.SHA3_512,
        KeySize
    ));

    public bool Validate(string value, string salt, string hash) => CryptographicOperations.FixedTimeEquals
    (
        Convert.FromBase64String(Create(value, salt)),
        Convert.FromBase64String(hash)
    );
}
