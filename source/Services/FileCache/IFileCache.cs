namespace DotNetCore.Services;

public interface IFileCache
{
    void Clear(string file);

    T Set<T>(string file, TimeSpan expiration, T value);

    bool TryGetValue<T>(string file, out T value);
}
