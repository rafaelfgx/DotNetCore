using System;

namespace DotNetCore.Security
{
    public class HashService : IHashService
    {
        public string Create(string value, string salt)
        {
            using var key = KeyGenerator.Generate(value, salt);

            return Convert.ToBase64String(key.GetBytes(512));
        }
    }
}
