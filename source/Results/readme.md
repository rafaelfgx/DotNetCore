# DotNetCore.Results

## Result

```cs
public record Result(HttpStatusCode Status)
{
    public Result(HttpStatusCode status, string message) { }

    public string Message { get; }

    public bool HasMessage { }

    public Result<T> Convert<T>() { }
}
```

## Result<T>

```cs
public sealed record Result<T> : Result
{
    public Result(HttpStatusCode status) { }

    public Result(HttpStatusCode status, T value) { }

    public Result(HttpStatusCode status, string message) { }

    public T Value { get; }

    public bool HasValue { }

    public Result Convert() { }
}
```
