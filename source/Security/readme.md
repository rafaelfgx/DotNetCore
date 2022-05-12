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

## JWT

### IJwtService

```cs
public interface IJwtService
{
    Dictionary<string, object> Decode(string token);

    string Encode(IList<Claim> claims);
}
```

### JwtService

```cs
public class JwtService : IJwtService
{
    public JwtService(JwtSettings settings) { }

    public Dictionary<string, object> Decode(string token) { }

    public string Encode(IList<Claim> claims) { }
}
```

### JwtSettings

```cs
public class JwtSettings
{
    public JwtSettings(string key, TimeSpan expires) { }

    public JwtSettings(string key, TimeSpan expires, string audience, string issuer) : this(key, expires) { }

    public string Audience { get; }

    public TimeSpan Expires { get; }

    public string Issuer { get; }

    public SecurityKey SecurityKey { get; }

    public TokenValidationParameters TokenValidationParameters { get; }
}
```

## Extensions

```cs
public static class Extensions
{
    public static void AddCryptographyService(this IServiceCollection services, string key) { }

    public static void AddHashService(this IServiceCollection services) { }

    public static void AddJwtService(this IServiceCollection services, JwtSettings settings) { }
}
```
