using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Scrutor;
using System.Reflection;

namespace DotNetCore.IoC;

public static class Extensions
{
    public static T AddAppSettings<T>(this IServiceCollection services) where T : class
    {
        var appSettings = new ConfigurationBuilder().Configuration().Get<T>();

        services.AddSingleton(appSettings);

        return appSettings;
    }

    public static T AddAppSettings<T>(this IServiceCollection services, string section) where T : class
    {
        var appSettings = new ConfigurationBuilder().Configuration().GetSection(section).Get<T>();

        services.AddSingleton(appSettings);

        return appSettings;
    }

    public static void AddClassesMatchingInterfaces(this IServiceCollection services, string @namespace)
    {
        var assemblies = DependencyContext.Default.GetDefaultAssemblyNames().Where(assembly => assembly.FullName.StartsWith(@namespace)).Select(Assembly.Load);

        services.Scan(scan => scan.FromAssemblies(assemblies).AddClasses().UsingRegistrationStrategy(RegistrationStrategy.Skip).AsMatchingInterface().WithScopedLifetime());
    }

    public static IConfigurationRoot Configuration(this IConfigurationBuilder configuration) => configuration.AddJsonFile("AppSettings.json", false, true).AddEnvironmentVariables().Build();

    public static string GetConnectionString(this IServiceCollection services, string name) => services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString(name);
}
