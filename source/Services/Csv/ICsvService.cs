namespace DotNetCore.Services;

public interface ICsvService
{
    Task<List<T>> ReadAsync<T>(string path, char separator = ',') where T : new();

    Task WriteAsync<T>(IEnumerable<T> items, string path, char separator = ',');

    Task<MemoryStream> WriteAsync<T>(IEnumerable<T> items, char separator = ',');
}
