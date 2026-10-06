using DotNetCore.Objects;
using Microsoft.AspNetCore.Http;

namespace DotNetCore.AspNetCore;

public static class HttpRequestExtensions
{
    public static IList<BinaryFile> Files(this HttpRequest request) => [.. request.Form.Files.Select(file =>
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return new BinaryFile(Guid.NewGuid(), file.Name, memoryStream.ToArray(), file.Length, file.ContentType);
    })];
}
