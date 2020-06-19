using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace DotNetCore.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] Compress(this byte[] bytes)
        {
            using var output = new MemoryStream();
            using var stream = new BrotliStream(output, CompressionMode.Compress);
            stream.Write(bytes, 0, bytes.Length);

            return output.ToArray();
        }

        public static byte[] Decompress(this byte[] bytes)
        {
            using var output = new MemoryStream();
            using var input = new MemoryStream(bytes);
            using var stream = new BrotliStream(input, CompressionMode.Decompress);
            stream.CopyTo(output);

            return output.ToArray();
        }

        public static T Object<T>(this byte[] bytes)
        {
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes));
        }
    }
}
