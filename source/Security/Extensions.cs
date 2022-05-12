using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.Security;

public static class Extensions
{
    public static void AddICryptographyService(this IServiceCollection services, string key)
    {
        services.AddSingleton<ICryptographyService>(_ => new CryptographyService(key));
    }

    public static void AddHashService(this IServiceCollection services)
    {
        services.AddSingleton<IHashService, HashService>();
    }

    public static void AddJwtService(this IServiceCollection services, JwtSettings settings)
    {
        services.AddSingleton(_ => settings);

        services.AddSingleton<IJwtService, JwtService>();
    }
}
