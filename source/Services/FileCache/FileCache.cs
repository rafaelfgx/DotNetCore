using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetCore.Services;

public sealed class FileCache : IFileCache
{
    private readonly object _lock = new();

    public void Clear(string file) => File.Delete(file);

    public T Set<T>(string file, TimeSpan expiration, T value)
    {
        lock (_lock)
        {
            var content = new FileCacheContent<T>(value, DateTime.UtcNow.Add(expiration));

            var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            File.WriteAllText(file, JsonSerializer.Serialize(content, options));

            return value;
        }
    }

    public bool TryGetValue<T>(string file, out T value)
    {
        lock (_lock)
        {
            if (!File.Exists(file))
            {
                value = default;

                return false;
            }

            var content = JsonSerializer.Deserialize<FileCacheContent<T>>(File.ReadAllText(file));

            if (content is null)
            {
                value = default;

                return false;
            }

            if (DateTime.UtcNow >= content.Expiration)
            {
                File.Delete(file);

                value = default;

                return false;
            }

            value = content.Value;

            return true;
        }
    }
}
