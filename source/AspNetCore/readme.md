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

    public static void UseException(this IApplicationBuilder application) { }

    public static void UseLocalization(this IApplicationBuilder application, params string[] cultures) { }

    public static void ConfigureFormOptions(this IServiceCollection services) { }
}
```

### BinaryFileExtensions

```cs
public static class BinaryFileExtensions
{
    public static IActionResult FileResult(this Task<BinaryFile> binaryFile) { }
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
    public static IActionResult ApiResult<T>(this Result<T> result) { }

    public static IActionResult ApiResult<T>(this Task<Result<T>> result) { }

    public static IActionResult ApiResult(this Result result) { }

    public static IActionResult ApiResult(this Task<Result> result) { }

    public static IActionResult ApiResult<T>(this T result) { }

    public static IActionResult ApiResult<T>(this Task<T> result) { }
}
```

### ServiceCollectionExtensions

```cs
public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddAuthorizationPolicy(this IMvcBuilder builder) { }

    public static IServiceCollection AddCorsAllowAny(this IServiceCollection services) { }

    public static IServiceCollection AddFileExtensionContentTypeProvider(this IServiceCollection services) { }

    public static IMvcBuilder AddJsonOptions(this IMvcBuilder builder) { }

    public static void AddSwaggerDefault(this IServiceCollection services) { }

    public static IServiceCollection ConfigureFormOptionsMaxLengthLimit(this IServiceCollection services) { }
}
```
