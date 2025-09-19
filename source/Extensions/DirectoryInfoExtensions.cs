namespace DotNetCore.Extensions;

public static class DirectoryInfoExtensions
{
    public static FileInfo GetFile(this DirectoryInfo directoryInfo, string name) => directoryInfo?.GetFiles(string.Concat(name, ".", "*")).SingleOrDefault();
}
