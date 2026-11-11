using System.Reflection;

namespace DotNetCore.Extensions;

public static class AssemblyExtensions
{
    public static FileInfo FileInfo(this Assembly assembly) => new(assembly.Location);
}
