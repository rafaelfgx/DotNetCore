using DotNetCore.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace DotNetCore.AspNetCore;

public static class BinaryFileExtensions
{
    public static IActionResult FileResult(this Task<BinaryFile> binaryFile)
    {
        if (binaryFile is null) return new NotFoundResult();

        var file = binaryFile.Result;

        if (file is null) return new NotFoundResult();

        new FileExtensionContentTypeProvider().TryGetContentType(file.ContentType, out var contentType);

        return contentType is null ? new NotFoundResult() : new FileContentResult(file.Bytes, contentType) { FileDownloadName = file.Name };
    }
}
