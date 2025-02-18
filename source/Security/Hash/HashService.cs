namespace DotNetCore.Security;

public class HashService : IHashService
{
    public string Create(string value, string salt)
    {
        using var key = KeyGenerator.Generate(value, salt);

        return Convert.ToBase64String(key.GetBytes(512));
    }

    public bool Validate(string hash, string value, string salt) => hash == Create(value, salt);
}
