# DotNetCore.IoC

## Extensions

```cs
public static class Extensions
{
    public static T AddAppSettings<T>(this IServiceCollection services) where T : class { }

    public static T AddAppSettings<T>(this IServiceCollection services, string section) where T : class { }

    public static void AddClassesMatchingInterfaces(this IServiceCollection services, string @namespace) { }

    public static IConfigurationRoot Configuration(this IConfigurationBuilder configuration) { }

    public static string GetConnectionString(this IServiceCollection services, string name) { }
}
```
