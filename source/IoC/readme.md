# DotNetCore.IoC

## Extensions

```cs
public static class Extensions
{
    public static T AddAppSettings<T>(this IServiceCollection services) where T : class { }

    public static void AddClassesMatchingInterfaces(this IServiceCollection services, params Assembly[] assemblies) { }

    public static IConfigurationRoot Configuration(this IConfigurationBuilder configuration) { }

    public static string GetConnectionString(this IServiceCollection services, string name) { }
}
```
