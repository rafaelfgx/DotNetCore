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
    public CryptographyService(string key) { }

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
}
```

### HashService

```cs
public class HashService : IHashService
{
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

    public SecurityKey SecurityKey { get; }

    public TokenValidationParameters TokenValidationParameters() { }
}
```

## Extensions

```cs
public static class Extensions
{
    public static void AddCryptographyService(this IServiceCollection services, string key) { }

    public static void AddHashService(this IServiceCollection services) { }

    public static void AddJsonWebTokenService(this IServiceCollection services, string key, TimeSpan expires) { }

    public static void AddJsonWebTokenService(this IServiceCollection services, string key, TimeSpan expires, string audience, string issuer) { }

    public static void AddJsonWebTokenService(this IServiceCollection services, JsonWebTokenSettings jsonWebTokenSettings) { }
}
```
