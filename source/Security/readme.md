# DotNetCore.Security

## Cryptography

### ICryptographyService

```cs
public interface ICryptographyService
{
    string Decrypt(string value, string salt);

    string Encrypt(string value, string salt);
}
```

### CryptographyService

```cs
public class CryptographyService : ICryptographyService
{
    public CryptographyService(string password) { }

    public string Decrypt(string value, string salt) { }

    public string Encrypt(string value, string salt) { }
}
```

## Hash

### IHashService

```cs
public interface IHashService
{
    string Create(string value, string salt);

    bool Validate(string value, string salt, string hash);
}
```

### HashService

```cs
public class HashService : IHashService
{
    public string Create(string value, string salt) { }

    public bool Validate(string value, string salt, string hash) { }
}
```

## Extensions

```cs
public static class Extensions
{
    public static void AddCryptographyService(this IServiceCollection services, string password) { }

    public static void AddHashService(this IServiceCollection services) { }
}
```
