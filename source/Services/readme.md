# DotNetCore.Services

## CsvService

### ICsvService

```cs
public interface ICsvService
{
    Task<List<T>> ReadAsync<T>(string path, char separator = ',') where T : new();

    Task WriteAsync<T>(IEnumerable<T> items, string path, char separator = ',');

    Task<MemoryStream> WriteAsync<T>(IEnumerable<T> items, char separator = ',');
}
```

### CsvService

```cs
public class CsvService : ICsvService
{
    public async Task<List<T>> ReadAsync<T>(string path, char separator = ',') where T : new() { }

    public async Task WriteAsync<T>(IEnumerable<T> items, string path, char separator = ',') { }

    public async Task<MemoryStream> WriteAsync<T>(IEnumerable<T> items, char separator = ',') { }
}
```

### Example

```cs
public sealed record Person
{
    public int Id { get; set; }

    public string Name { get; set; }
}
```

```cs
public static void Main()
{
    var people = new List<Person>();

    var stream = WriteAsync(people).Result;

    WriteAsync(people, "People.csv").Wait();

    people = ReadAsync<Person>("People.csv").Result;
}
```

## FileCache

### IFileCache

```cs
public interface IFileCache
{
    void Clear(string file);

    T Set<T>(string file, TimeSpan expiration, T value);

    bool TryGetValue<T>(string file, out T value);
}
```

### FileCache

```cs
public sealed class FileCache : IFileCache
{
    public void Clear(string file) { }

    public T Set<T>(string file, TimeSpan expiration, T value) { }

    public bool TryGetValue<T>(string file, out T value) { }
}
```

## Http

### HttpOptions

```cs
public sealed record HttpOptions
{
    public string BaseAddress { get; set; }

    public AuthenticationHeaderValue Authentication { get; set; }

    public int TimeoutSeconds { get; set; } = 5;

    public int RetryCount { get; set; }

    public int RetrySeconds { get; set; }
}
```

### IHttpService

```cs
public interface IHttpService
{
    Task<HttpStatusCode> DeleteAsync(string uri);

    Task<Tuple<HttpStatusCode, TResponse>> GetAsync<TResponse>(string uri);

    Task<HttpStatusCode> PatchAsync(string uri, object value);

    Task<HttpStatusCode> PostAsync(string uri, object value);

    Task<Tuple<HttpStatusCode, TResponse>> PostAsync<TResponse>(string uri, object value);

    Task<HttpStatusCode> PutAsync(string uri, object value);
}
```

### HttpService

```cs
public abstract class HttpService : IHttpService
{
    public async Task<HttpStatusCode> DeleteAsync(string uri) { }

    public async Task<Tuple<HttpStatusCode, TResponse>> GetAsync<TResponse>(string uri) { }

    public async Task<HttpStatusCode> PatchAsync(string uri, object value) { }

    public async Task<HttpStatusCode> PostAsync(string uri, object value) { }

    public async Task<Tuple<HttpStatusCode, TResponse>> PostAsync<TResponse>(string uri, object value) { }

    public async Task<HttpStatusCode> PutAsync(string uri, object value) { }
}
```

### Example

```cs
public interface ITestHttpService : IHttpService { }
```

```cs
public sealed record TestHttpService(HttpOptions options) : HttpService(options), ITestHttpService;
```

```cs
public sealed record Todo(int Id, string Title);
```

```cs
public class Program
{
    public static void Main()
    {
        var baseAddress = "https://jsonplaceholder.typicode.com";

        var options = new HttpOptions { BaseAddress = baseAddress };

        ITestHttpService httpService = new TestHttpService(options);

        var services = new ServiceCollection();

        services.AddScoped<ITestHttpService>(provider => httpService);

        httpService = services.BuildServiceProvider().GetRequiredService<ITestHttpService>();

        var deleteError = httpService.DeleteAsync("todo/1").Result;

        var deleteSuccess = httpService.DeleteAsync("todos/1").Result;

        var listError = httpService.GetAsync<IEnumerable<Todo>>("todo").Result;

        var listSuccess = httpService.GetAsync<IEnumerable<Todo>>("todos").Result;

        var getError = httpService.GetAsync<Todo>("todo/1").Result;

        var getSuccess = httpService.GetAsync<Todo>("todos/1").Result;

        var postError = httpService.PostAsync("todo", default).Result;

        var postSuccess = httpService.PostAsync("todos", new { Title = "Title" }).Result;

        var postResultError = httpService.PostAsync<Todo>("todo", default).Result;

        var postResultSuccess = httpService.PostAsync<Todo>("todos", new { Title = "Title" }).Result;

        var putError = httpService.PutAsync("todo/1", default).Result;

        var putSuccess = httpService.PutAsync("todos/1", new { Title = "Title" }).Result;
    }
}
```

## JsonStringLocalizer

```cs
public class JsonStringLocalizer : IStringLocalizer
{
   public JsonStringLocalizer(string path) { }
}
```

## Extensions

```cs
public static class Extensions
{
    public static void AddCsvService(this IServiceCollection services) { }

    public static void AddFileCache(this IServiceCollection services) { }

    public static void AddJsonStringLocalizer(this IServiceCollection services) { }
}
```
