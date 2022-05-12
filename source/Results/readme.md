# DotNetCore.Results

## IResult

```cs
public interface IResult
{
    bool Failed { get; }

    string Message { get; }

    bool Succeeded { get; }
}
```

## IResult<T>

```cs
public interface IResult<out T> : IResult
{
    T Data { get; }
}
```

## Result

```cs
public class Result : IResult
{
    protected Result() { }

    public bool Failed { get; }

    public string Message { get; }

    public bool Succeeded { get; }

    public static IResult Fail() { }

    public static IResult Fail(string message) { }

    public static IResult Success() { }

    public static IResult Success(string message) { }
}
```

## Result<T>

```cs
public class Result<T> : Result, IResult<T>
{
    protected Result() { }

    public T Data { get; }

    public static new IResult<T> Fail() { }

    public static new IResult<T> Fail(string message) { }

    public static new IResult<T> Success() { }

    public static new IResult<T> Success(string message) { }

    public static IResult<T> Success(T data) { }

    public static IResult<T> Success(T data, string message) { }
}
```

## Extensions

```cs
public static class Extensions
{
    public static IResult<T> Fail<T>(this IResult result) { }

    public static IResult<T> Success<T>(this T data) { }

    public static IResult<T> Success<T>(this T data, string message) { }
}
```
