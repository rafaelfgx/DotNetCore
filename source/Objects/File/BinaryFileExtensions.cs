namespace DotNetCore.Objects;

public static class BinaryFileExtensions
{
    public static async Task<IEnumerable<BinaryFile>> SaveAsync(this IEnumerable<BinaryFile> files, string directory)
    {
        if (string.IsNullOrWhiteSpace(directory)) return null;

        var binaryFiles = files as BinaryFile[] ?? files.ToArray();

        await Task.WhenAll(binaryFiles.Select(file => file.SaveAsync(directory)));

        return binaryFiles;
    }
}
