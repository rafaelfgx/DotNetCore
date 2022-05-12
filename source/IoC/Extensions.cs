using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace DotNetCore.IoC;

public static class Extensions
{
    public static T AddAppSettings<T>(this IServiceCollection services) where T : class
    {
        var appSettings = services.BuildServiceProvider().GetRequiredService<IConfiguration>().Get<T>();

        services.AddSingleton(appSettings);

        return appSettings;
    }

    public static void AddClassesMatchingInterfaces(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
    }

    public static IConfigurationRoot Configuration(this IConfigurationBuilder configuration)
    {
        return configuration
            .AddJsonFile("AppSettings.json", false, true)
            .AddEnvironmentVariables()
            .Build();
    }

    public static string GetConnectionString(this IServiceCollection services, string name)
    {
        return services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetConnectionString(name);
    }
}
