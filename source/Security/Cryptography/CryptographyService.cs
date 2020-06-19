using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNetCore.Security
{
    public class CryptographyService : ICryptographyService
    {
        public CryptographyService(string key)
        {
            Key = key;
        }

        private string Key { get; }

        public string Decrypt(string value, string salt)
        {
            using var algorithm = Algorithm(salt);

            return Encoding.Unicode.GetString(Transform(Convert.FromBase64String(value), algorithm.CreateDecryptor()));
        }

        public string Decrypt(string value)
        {
            return Decrypt(value, string.Empty);
        }

        public string Encrypt(string value, string salt)
        {
            using var algorithm = Algorithm(salt);

            return Convert.ToBase64String(Transform(Encoding.Unicode.GetBytes(value), algorithm.CreateEncryptor()));
        }

        public string Encrypt(string value)
        {
            return Encrypt(value, string.Empty);
        }

        private static byte[] Transform(byte[] bytes, ICryptoTransform cryptoTransform)
        {
            using (cryptoTransform)
            {
                using var memoryStream = new MemoryStream();

                using var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);

                cryptoStream.Write(bytes, 0, bytes.Length);

                cryptoStream.Close();

                return memoryStream.ToArray();
            }
        }

        private SymmetricAlgorithm Algorithm(string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                salt = Key;
            }

            using var key = new Rfc2898DeriveBytes(Key, Encoding.ASCII.GetBytes(salt));

            var algorithm = new RijndaelManaged();

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);

            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            return algorithm;
        }
    }
}
