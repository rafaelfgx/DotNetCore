# DotNetCore.Security

## Cryptography

### ICryptographyService

```cs
public interface ICryptographyService
{
    string Decrypt(string value);

    string Decrypt(string value, string salt);

    string Encrypt(string value);

    string Encrypt(string value, string salt);
}
```

### CryptographyService

```cs
public class CryptographyService : ICryptographyService
{
    public CryptographyService(string key) { }

    public string Decrypt(string value, string salt) { }

    public string Decrypt(string value) { }

    public string Encrypt(string value, string salt) { }

    public string Encrypt(string value) { }
}
```

## Hash

### IHashService

```cs
public interface IHashService
{
    string Create(string value, string salt);
}
```

### HashService

```cs
public class HashService : IHashService
{
    public Hash(int iterations, int size) { }

    public string Create(string value, string salt) { }
}
```

## JsonWebToken

### IJsonWebTokenService

```cs
public interface IJsonWebTokenService
{
    Dictionary<string, object> Decode(string token);

    string Encode(IList<Claim> claims);
}
```

### JsonWebTokenService

```cs
public class JsonWebTokenService : IJsonWebTokenService
{
    public JsonWebTokenService(JsonWebTokenSettings jsonWebTokenSettings) { }

    public Dictionary<string, object> Decode(string token) { }

    public string Encode(IList<Claim> claims) { }
}
```

### JsonWebTokenSettings

```cs
public class JsonWebTokenSettings
{
    public JsonWebTokenSettings(string key, TimeSpan expires) { }

    public JsonWebTokenSettings(string key, TimeSpan expires, string audience, string issuer) : this(key, expires) { }

    public string Audience { get; }

    public TimeSpan Expires { get; }

    public string Issuer { get; }

    public string Key { get; }
}
```

## Extensions

```cs
public static class Extensions
{
    public static void AddCryptography(this IServiceCollection services, string key) { }

    public static void AddHash(this IServiceCollection services, int iterations, int size) { }

    public static void AddJsonWebToken(this IServiceCollection services, string key, TimeSpan expires) { }

    public static void AddJsonWebToken(this IServiceCollection services, string key, TimeSpan expires, string audience, string issuer) { }

    public static void AddJsonWebToken(this IServiceCollection services, JsonWebTokenSettings jsonWebTokenSettings) { }
}
```
