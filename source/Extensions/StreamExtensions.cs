namespace DotNetCore.Extensions;

public static class StreamExtensions
{
    public static string ToBase64String(this Stream stream)
    {
        using var memoryStream = new MemoryStream();

        stream.CopyTo(memoryStream);

        return Convert.ToBase64String(memoryStream.ToArray());
    }
}
