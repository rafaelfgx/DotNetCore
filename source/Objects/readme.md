# DotNetCore.Objects

## BinaryFile

```cs
public class BinaryFile
{
    public BinaryFile(Guid id, string name, byte[] bytes, long length, string contentType) { }

    public Guid Id { get; }

    public string Name { get; }

    public byte[] Bytes { get; private set; }

    public long Length { get; }

    public string ContentType { get; }

    public static async Task<BinaryFile> ReadAsync(string directory, Guid id) { }

    public async Task SaveAsync(string directory) { }
}
```

## BinaryFileExtensions

```cs
public static class BinaryFileExtensions
{
    public static async Task<IEnumerable<BinaryFile>> SaveAsync(this IEnumerable<BinaryFile> files, string directory) { }
}
```

## Filter

```cs
public sealed class Filter
{
    public string Property { get; set; }

    public string Comparison { get; set; }

    public string Value { get; set; }
}
```

## Filters

```cs
public sealed class Filters : List<Filter> { }
```

## Order

```cs
public class Order
{
    public bool Ascending { get; set; }

    public string Property { get; set; }
}
```

## Page

```cs
public class Page
{
    public int Index { get; set; }

    public int Size { get; set; }
}
```

## GridParameters

```cs
public class GridParameters
{
    public Filters Filters { get; set; }

    public Order Order { get; set; }

    public Page Page { get; set; }
}
```

## Grid

```cs
public class Grid<T>
{
    public Grid(IQueryable<T> queryable, GridParameters parameters) { }

    public long Count { get; }

    public IEnumerable<T> List { get; }

    public GridParameters Parameters { get; }
}
```

## GridExtensions

```cs
public static class GridExtensions
{
    public static Grid<T> Grid<T>(this IQueryable<T> queryable, GridParameters parameters) { }

    public static Task<Grid<T>> GridAsync<T>(this IQueryable<T> queryable, GridParameters parameters) { }
}
```
