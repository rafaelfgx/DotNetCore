using System.Globalization;

namespace DotNetCore.Services;

public class CsvService : ICsvService
{
    public async Task<List<T>> ReadAsync<T>(string path, char separator = ',') where T : new()
    {
        if (!File.Exists(path)) return new List<T>();

        var lines = await File.ReadAllLinesAsync(path);

        if (lines.Length < 2) return new List<T>();

        var headers = lines.First().Split(separator);

        if (!headers.Any()) return new List<T>();

        var items = new List<T>();

        foreach (var line in lines.Skip(1))
        {
            var values = line.Split(separator);

            var item = new T();

            for (var i = 0; i < headers.Length; i++)
            {
                var property = item.GetType().GetProperty(headers[i]);

                if (property is null) continue;

                var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                var value = Convert.ChangeType(values[i], type, CultureInfo.InvariantCulture);

                property.SetValue(item, value);
            }

            items.Add(item);
        }

        return items;
    }

    public async Task WriteAsync<T>(IEnumerable<T> items, string path, char separator = ',')
    {
        using var stream = await WriteAsync(items);

        await File.WriteAllBytesAsync(path, stream.ToArray());
    }

    public async Task<MemoryStream> WriteAsync<T>(IEnumerable<T> items, char separator = ',')
    {
        var enumerable = items.ToList();

        if (!enumerable.Any()) return new MemoryStream();

        var properties = typeof(T).GetProperties();

        var memoryStream = new MemoryStream();

        await using var streamWriter = new StreamWriter(memoryStream);

        await streamWriter.WriteLineAsync(string.Join(separator, properties.Select(property => property.Name)));

        enumerable.ForEach(item => streamWriter.WriteLine(string.Join(separator, properties.Select(property => property.GetValue(item)))));

        await streamWriter.FlushAsync();

        await memoryStream.FlushAsync();

        memoryStream.Position = 0;

        return memoryStream;
    }
}
