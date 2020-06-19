# DotNetCore.AspNetCore

## Attributes

### EnumAuthorizeAttribute

```cs
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class EnumAuthorizeAttribute : AuthorizeAttribute
{
    public EnumAuthorizeAttribute(params object[] roles) { }
}
```

## Extensions

### ApplicationBuilderExtensions

```cs
public static class ApplicationBuilderExtensions
{
    public static void UseCorsAllowAny(this IApplicationBuilder application) { }

    public static void UseEndpoints(this IApplicationBuilder application) { }

    public static void UseException(this IApplicationBuilder application) { }

    public static void UseHttps(this IApplicationBuilder application) { }

    public static void UseSpaAngular(this IApplicationBuilder application, string sourcePath, string npmScript, string baseUri) { }

    public static void UseSpaAngularServer(this IApplicationBuilder application, string sourcePath, string npmScript) { }

    public static void UseSpaProxyServer(this IApplicationBuilder application, string sourcePath, string baseUri) { }

    public static void ConfigureFormOptions(this IServiceCollection services) { }
}
```

### HostBuilderExtensions

```cs
public static class HostBuilderExtensions
{
    public static void Run<T>(this IHostBuilder host) where T : class { }
}
```

### HttpRequestExtensions

```cs
public static class HttpRequestExtensions
{
    public static IList<BinaryFile> Files(this HttpRequest request) { }
}
```

### ResultExtensions

```cs
public static class ResultExtensions
{
    public static async Task<IActionResult> ResultAsync<T>(this Task<IResult<T>> result) { }

    public static async Task<IActionResult> ResultAsync(this Task<IResult> result) { }

    public static async Task<IActionResult> ResultAsync<T>(this Task<T> result) { }
}
```

### ServiceCollectionExtensions

```cs
public static class ServiceCollectionExtensions
{
    public static void AddAuthenticationJwtBearer(this IServiceCollection services) { }

    public static void AddControllersDefault(this IServiceCollection services) { }

    public static void AddSpaStaticFiles(this IServiceCollection services, string rootPath) { }

    public static void AddFileExtensionContentTypeProvider(this IServiceCollection services) { }

    public static void ConfigureFormOptions(this IServiceCollection services) { }
}
```

## Action Results

### ApiResult

```cs
public class ApiResult : IActionResult
{
    public static IActionResult Create(IResult result) { }

    public static IActionResult Create(object data) { }
}
```
