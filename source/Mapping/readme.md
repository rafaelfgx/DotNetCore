# DotNetCore.Mapping

```cs
public static class Extensions
{
    public static TSource Clone<TSource>(this TSource source) { }

    public static TDestination Map<TSource, TDestination>(this TSource source) { }

    public static TDestination Map<TDestination>(this object source) { }

    public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination) { }

    public static IQueryable<TDestination> Project<TSource, TDestination>(this IQueryable<TSource> queryable) { }
}
```
