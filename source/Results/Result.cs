using System.Net;

namespace DotNetCore.Results;

public record Result(HttpStatusCode Status)
{
    public Result(HttpStatusCode status, string message) : this(status) => Message = message;

    public string Message { get; }

    public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

    public Result<T> Convert<T>() => new(Status, Message);
}

public sealed record Result<T> : Result
{
    public Result(HttpStatusCode status) : base(status) { }

    public Result(HttpStatusCode status, T value) : base(status) { Value = value; }

    public Result(HttpStatusCode status, string message) : base(status, message) { }

    public T Value { get; }

    public bool HasValue => Value is not null;

    public Result Convert() => new(Status, Message);
}
