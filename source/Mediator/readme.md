# DotNetCore.Mediator

The smallest, simplest and fastest implementation of the mediator pattern.

## ASP.NET Core Example

```cs
builder.Services.AddMediator(nameof(Namespace));
```

```cs
public sealed class Controller(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public Task<Result<long>> Add(AddRequest request) => mediator.HandleAsync<AddRequest, long>(request);

    [HttpDelete("{id:long}")]
    public Task<Result> Delete(long id) => mediator.HandleAsync(new DeleteRequest(id));

    [HttpGet("{id:long}")]
    public Task<Result<Dto>> Get(long id) => mediator.HandleAsync<GetRequest, Dto>(new GetRequest(id));

    [HttpGet]
    public Task<Result<IEnumerable<Dto>>> List() => mediator.HandleAsync<ListRequest, IEnumerable<Dto>>(new ListRequest());

    [HttpPut("{id:long}")]
    public Task<Result> Update(long id, UpdateRequest request) => mediator.HandleAsync(request with { Id = id });
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
