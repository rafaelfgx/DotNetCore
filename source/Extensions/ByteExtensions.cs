using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace DotNetCore.Extensions;

public static class ByteExtensions
{
    public static byte[] Compress(this byte[] bytes)
    {
        if (bytes is null) return Array.Empty<byte>();

        using var output = new MemoryStream();

        using var stream = new BrotliStream(output, CompressionMode.Compress);

        stream.Write(bytes, 0, bytes.Length);

        return output.ToArray();
    }

    public static byte[] Decompress(this byte[] bytes)
    {
        using var input = new MemoryStream(bytes);

        using var stream = new BrotliStream(input, CompressionMode.Decompress);

        using var output = new MemoryStream();

        stream.CopyTo(output);

        return output.ToArray();
    }

    public static T Object<T>(this byte[] bytes) => JsonSerializer.Deserialize<T>(Encoding.Default.GetString(bytes));
}
