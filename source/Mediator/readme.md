# DotNetCore.Mediator

The smallest, simplest and fastest implementation of the mediator pattern.

## ASP.NET Core Startup Example

```cs
public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediator("Namespace");
    }
}
```

## ASP.NET Core Controller Example

```cs
public sealed class Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public Controller(IMediator mediator)
    {
        _mediator = mediator;
    }
}
```

## Mediator

```cs
public interface IMediator
{
    Task<Result> HandleAsync<TRequest>(TRequest request);

    Task<Result<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request);
}
```

```cs
public sealed class Mediator : IMediator
{
    public async Task<Result> HandleAsync<TRequest>(TRequest request) { }

    public async Task<Result<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request) { }
}
```

```cs
public interface IHandler<in TRequest>
{
    Task<Result> HandleAsync(TRequest request);
}
```

```cs
public interface IHandler<in TRequest, TResponse>
{
    Task<Result<TResponse>> HandleAsync(TRequest request);
}
```
