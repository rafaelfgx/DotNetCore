using System.Security.Cryptography;
using System.Text;

namespace DotNetCore.Security;

public static class KeyGenerator
{
    public static DeriveBytes Generate(string password, string salt) => new Rfc2898DeriveBytes(password, Encoding.Default.GetBytes(salt), 10000, HashAlgorithmName.SHA512);
}
