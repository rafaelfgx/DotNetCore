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

    public bool Failed { get; protected set; }

    public string Message { get; protected set; }

    public bool Succeeded { get; protected set; }

    public static IResult Fail() { }

    public static IResult Fail(string message) { }

    public static Task<IResult> FailAsync() { }

    public static Task<IResult> FailAsync(string message) { }

    public static IResult Success() { }

    public static IResult Success(string message) { }

    public static Task<IResult> SuccessAsync() { }

    public static Task<IResult> SuccessAsync(string message) { }
}
```

## Result<T>

```cs
public class Result<T> : Result, IResult<T>
{
    protected Result() { }

    public T Data { get; private set; }

    public static new IResult<T> Fail() { }

    public static new IResult<T> Fail(string message) { }

    public static new Task<IResult<T>> FailAsync() { }

    public static new Task<IResult<T>> FailAsync(string message) { }

    public static new IResult<T> Success() { }

    public static new IResult<T> Success(string message) { }

    public static IResult<T> Success(T data) { }

    public static IResult<T> Success(T data, string message) { }

    public static new Task<IResult<T>> SuccessAsync() { }

    public static new Task<IResult<T>> SuccessAsync(string message) { }

    public static Task<IResult<T>> SuccessAsync(T data) { }

    public static Task<IResult<T>> SuccessAsync(T data, string message) { }
}
```
