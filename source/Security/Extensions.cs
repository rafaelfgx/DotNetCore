using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.Security;

public static class Extensions
{
    public static void AddCryptographyService(this IServiceCollection services, string key) => services.AddSingleton<ICryptographyService>(_ => new CryptographyService(key));

    public static void AddHashService(this IServiceCollection services) => services.AddSingleton<IHashService, HashService>();

    public static void AddJwtService(this IServiceCollection services) => services.AddSingleton<IJwtService, JwtService>();
}
