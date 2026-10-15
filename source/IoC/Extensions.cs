using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Scrutor;
using System.Reflection;

namespace DotNetCore.IoC;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        public T AddAppSettings<T>() where T : class
        {
            var appSettings = new ConfigurationBuilder().Configuration().Get<T>();

            services.AddSingleton(appSettings);

            return appSettings;
        }

        public T AddAppSettings<T>(string section) where T : class
        {
            var appSettings = new ConfigurationBuilder().Configuration().GetSection(section).Get<T>();

            services.AddSingleton(appSettings);

            return appSettings;
        }

        public void AddClassesMatchingInterfaces(string @namespace)
        {
            if (DependencyContext.Default == null) return;

            var assemblies = DependencyContext.Default.GetDefaultAssemblyNames().Where(assembly => assembly.FullName.StartsWith(@namespace)).Select(Assembly.Load);

            services.Scan(scan => scan.FromAssemblies(assemblies).AddClasses().UsingRegistrationStrategy(RegistrationStrategy.Skip).AsMatchingInterface().WithScopedLifetime());
        }

        public string GetConnectionString(string name) => services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString(name);
    }

    public static IConfigurationRoot Configuration(this IConfigurationBuilder configuration) => configuration.AddJsonFile("AppSettings.json", false, true).AddEnvironmentVariables().Build();
}
