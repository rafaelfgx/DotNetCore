using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace DotNetCore.Extensions;

public static class ByteExtensions
{
    extension(byte[] bytes)
    {
        public byte[] Compress()
        {
            if (bytes is null) return [];

            using var output = new MemoryStream();

            using var stream = new BrotliStream(output, CompressionMode.Compress);

            stream.Write(bytes, 0, bytes.Length);

            return output.ToArray();
        }

        public byte[] Decompress()
        {
            using var input = new MemoryStream(bytes);

            using var stream = new BrotliStream(input, CompressionMode.Decompress);

            using var output = new MemoryStream();

            stream.CopyTo(output);

            return output.ToArray();
        }

        public T Object<T>() => JsonSerializer.Deserialize<T>(Encoding.Default.GetString(bytes));
    }
}
