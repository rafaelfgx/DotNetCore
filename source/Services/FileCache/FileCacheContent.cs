namespace DotNetCore.Services;

public sealed record FileCacheContent<T>(T Value, DateTime Expiration);
