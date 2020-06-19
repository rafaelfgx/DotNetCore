# DotNetCore.Extensions

## ByteExtensions

```cs
public static class ByteExtensions
{
    public static byte[] Compress(this byte[] bytes) { }

    public static byte[] Decompress(this byte[] bytes) { }

    public static T Object<T>(this byte[] bytes) { }
}
```

## ClaimExtensions

```cs
public static class ClaimExtensions
{
    public static void AddJti(this ICollection<Claim> claims) { }

    public static void AddRoles(this ICollection<Claim> claims, string[] roles) { }

    public static void AddSub(this ICollection<Claim> claims, string sub) { }
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

## StringExtensions

```cs
public static class StringExtensions
{
    public static string CamelCase(this string value) { }

    public static string NonSpecialCharacters(this string value) { }

    public static string NumericCharacters(this string value) { }
}
```
