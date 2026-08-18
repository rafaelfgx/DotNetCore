# DotNetCore.Extensions

## AssemblyExtensions

```cs
public static class AssemblyExtensions
{
    public static FileInfo FileInfo(this Assembly assembly) { }
}
```

## ByteExtensions

```cs
public static class ByteExtensions
{
    public static byte[] Compress(this byte[] bytes) { }

    public static byte[] Decompress(this byte[] bytes) { }

    public static T Object<T>(this byte[] bytes) { }
}
```

## ClaimsPrincipalExtensions

```cs
public static class ClaimsPrincipalExtensions
{
    public static Claim Claim(this ClaimsPrincipal claimsPrincipal, string claimType) { }

    public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal) { }

    public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType) { }

    public static string ClaimSub(this ClaimsPrincipal claimsPrincipal) { }

    public static long Id(this ClaimsPrincipal claimsPrincipal) { }

    public static IEnumerable<T> Roles<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum { }

    public static T RolesFlag<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum { }
}
```

## DateTimeExtensions

```cs
public static class DateTimeExtensions
{
    public static List<(DateTime, DateTime)> Chunks(this DateTime startDate, DateTime endDate, int days) { }
}
```

## DirectoryInfoExtensions

```cs
public static class DirectoryInfoExtensions
{
    public static FileInfo GetFile(this DirectoryInfo directoryInfo, string name) { }
}
```

## EnumExtensions

```cs
public static class EnumExtensions
{
    public static string GetDescription(this Enum value) { }

    public static string[] ToArray(this Enum value) { }
}
```

## ObjectExtensions

```cs
public static class ObjectExtensions
{
    public static byte[] Bytes(this object obj) { }

    public static Dictionary<string, object> Dictionary(this object obj) { }

    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(this object obj) where T : Attribute { }

    public static string Serialize(this object obj) { }

    public static void SetProperty(this object obj, string name, object value) { }
}
```

## QueryableExtensions

```cs
public static class QueryableExtensions
{
    public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string property, object value) { }

    public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, string property, string comparison, object value) { }

    public static IQueryable<T> Order<T>(this IQueryable<T> queryable, string property, bool ascending) { }

    public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int index, int size) { }
}
```

## StreamExtensions

```cs
public static class StreamExtensions
{
    public static string ToBase64String(this Stream stream) { }
}
```

## StringExtensions

```cs
public static class StringExtensions
{
    public static T Deserialize<T>(this string value) where T : class { }

    public static string ExtractNumbers(this string value) { }

    public static bool IsDate(this string value) { }

    public static bool IsEmail(this string value) { }

    public static bool IsInteger(this string value) { }

    public static bool IsLogin(this string value) { }

    public static bool IsNumber(this string value) { }

    public static bool IsPassword(this string value) { }

    public static bool IsTime(this string value) { }

    public static bool IsUrl(this string value) { }

    public static string RemoveAccents(this string value) { }

    public static string RemoveDuplicateSpaces(this string value) { }

    public static string RemoveSpecialCharacters(this string value) { }

    public static string Sanitize(this string value) { }
}
```
