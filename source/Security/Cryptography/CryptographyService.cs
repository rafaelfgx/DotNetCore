using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DotNetCore.Security
{
    public class CryptographyService : ICryptographyService
    {
        private readonly string _key;

        public CryptographyService(string key)
        {
            _key = key;
        }

        public string Decrypt(string value, string salt)
        {
            using var algorithm = Algorithm(salt);

            return Encoding.Default.GetString(Transform(Convert.FromBase64String(value), algorithm.CreateDecryptor()));
        }

        public string Encrypt(string value, string salt)
        {
            using var algorithm = Algorithm(salt);

            return Convert.ToBase64String(Transform(Encoding.Default.GetBytes(value), algorithm.CreateEncryptor()));
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
            using var key = KeyGenerator.Generate(_key, salt);

            var algorithm = new RijndaelManaged();

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);

            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            return algorithm;
        }
    }
}
