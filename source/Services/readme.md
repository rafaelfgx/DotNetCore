# DotNetCore.Services

## CsvService

### ICsvService

```cs
public interface ICsvService
{
    Task<IEnumerable<T>> ReadAsync<T>(string path, string separator) where T : new();

    Task WriteAsync<T>(string path, string separator, IEnumerable<T> items);
}
```

### CsvService

```cs
public class CsvService : ICsvService
{
    public async Task<IEnumerable<T>> ReadAsync<T>(string path, string separator) where T : new() { }

    public Task WriteAsync<T>(string path, string separator, IEnumerable<T> items) { }
}
```

### Example

```cs
public class Program
{
    public static void Main()
    {
        ICsvService csvService = new CsvService();

        var customers = csvService.ReadAsync<Customer>("Customers.csv", ";").Result;

        csvService.WriteAsync("Customers.csv", ";", customers).Wait();
    }
}
```
