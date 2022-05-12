namespace DotNetCore.Services;

public interface ICsvService
{
    Task<IEnumerable<T>> ReadAsync<T>(string path, string separator) where T : new();

    Task WriteAsync<T>(string path, string separator, IEnumerable<T> items);
}
