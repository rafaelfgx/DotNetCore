using DotNetCore.Extensions;

namespace DotNetCore.Objects;

public class BinaryFile(Guid id, string name, byte[] bytes, long length, string contentType)
{
    public Guid Id { get; } = id;

    public string Name { get; } = name;

    public byte[] Bytes { get; private set; } = bytes;

    public long Length { get; } = length;

    public string ContentType { get; } = contentType;

    public static async Task<BinaryFile> ReadAsync(string directory, Guid id)
    {
        if (!Directory.Exists(directory) || id == Guid.Empty) return null;

        var file = new DirectoryInfo(directory).GetFile(id.ToString());

        if (file is null) return null;

        var bytes = await File.ReadAllBytesAsync(file.FullName).ConfigureAwait(false);

        return new BinaryFile(id, file.Name, bytes, file.Length, file.Extension);
    }

    public async Task SaveAsync(string directory)
    {
        if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(Name) || Bytes is null || Bytes.LongLength == 0) return;

        Directory.CreateDirectory(directory);

        var name = string.Concat(Id, Path.GetExtension(Name));

        var path = Path.Combine(directory, name);

        await File.WriteAllBytesAsync(path, Bytes).ConfigureAwait(false);

        Bytes = default;
    }
}
