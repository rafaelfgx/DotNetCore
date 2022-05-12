# DotNetCore.Mediator

The smallest, simplest and fastest implementation of the mediator pattern.

## Tests

 ```csharp
[TestClass]
public class MediatorTests
{
    private readonly IMediator _mediator;

    public MediatorTests()
    {
        var services = new ServiceCollection();

        services.AddMediator(typeof(MediatorTests).Assembly);

        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
    }

    [TestMethod]
    public void CategoryByIdQuery()
    {
        var query = new CategoryByIdQuery(1);

        var response = _mediator.HandleAsync<CategoryByIdQuery, Category>(query).Result;

        Assert.IsTrue(response.Succeeded && response.Data != default);
    }

    [TestMethod]
    [ExpectedException(typeof(AggregateException))]
    public void DeleteCategoryCommandShouldThrowException()
    {
        var command = new DeleteCategoryCommand(1);

        _mediator.HandleAsync(command).Wait();
    }

    [TestMethod]
    public void InsertCategoryCommand()
    {
        var command = new InsertCategoryCommand("Name");

        var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

        Assert.IsTrue(response.Succeeded && response.Data > 0);
    }

    [TestMethod]
    public void InsertCategoryCommandShouldValidationFail()
    {
        var command = new InsertCategoryCommand(string.Empty);

        var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

        Assert.IsTrue(response.Failed);
    }

    [TestMethod]
    public void UpdateCategoryCommand()
    {
        var command = new UpdateCategoryCommand(1, "Name");

        var response = _mediator.HandleAsync(command).Result;

        Assert.IsTrue(response.Succeeded);
    }
}
 ```

 ## ASP.NET Core Startup Example

```cs
public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        /// AnyType = any type within the project containing commands, queries, events, validators and handlers
        services.AddMediator(typeof(AnyType).Assembly);
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

## Query

```cs
public sealed record CategoryByIdQuery(long Id);
```

## QueryHandler

```cs
public sealed class CategoryByIdQueryHandler : IHandler<CategoryByIdQuery, Category>
{
    public Task<IResult<Category>> HandleAsync(CategoryByIdQuery request)
    {
        var category = new Category(request.Id, nameof(Category));

        return Task.FromResult(category.Success());
    }
}
```

## Command

```cs
public sealed record InsertCategoryCommand(string Name);
```

## CommandHandler

```cs
public sealed class InsertCategoryCommandHandler : IHandler<InsertCategoryCommand, long>
{
    public Task<IResult<long>> HandleAsync(InsertCategoryCommand request)
    {
        return Task.FromResult(1L.Success());
    }
}
```

## CommandValidator

```cs
public class InsertCategoryCommandValidator : AbstractValidator<InsertCategoryCommand>
{
    public InsertCategoryCommandValidator()
    {
        RuleFor(category => category.Name).NotEmpty();
    }
}
```

## Command

```cs
public sealed record UpdateCategoryCommand(long Id, string Name);
```

## CommandHandler

```cs
public sealed class UpdateCategoryCommandHandler : IHandler<UpdateCategoryCommand>
{
    private readonly IMediator _mediator;

    public UpdateCategoryCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IResult> HandleAsync(UpdateCategoryCommand request)
    {
        var category = new Category(request.Id, request.Name);

        var updatedCategoryEvent = new UpdatedCategoryEvent(category);

        await _mediator.HandleAsync(updatedCategoryEvent).ConfigureAwait(false);

        return Result.Success();
    }
}
```

## Event

```cs
public sealed record UpdatedCategoryEvent(Category Category);
```

## EventHandler

```cs
public sealed class UpdatedCategoryEventHandler : IHandler<UpdatedCategoryEvent>
{
    public Task<IResult> HandleAsync(UpdatedCategoryEvent request)
    {
        return Task.FromResult(Result.Success());
    }
}
```

## Mediator

```cs
public interface IMediator
{
    Task<IResult> HandleAsync<TRequest>(TRequest request);

    Task<IResult<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request);
}
```

```cs
public sealed class Mediator : IMediator
{
    public async Task<IResult> HandleAsync<TRequest>(TRequest request) { }

    public async Task<IResult<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request) { }
}
```

```cs
public interface IHandler<TRequest>
{
    Task<IResult> HandleAsync(TRequest request);
}

public interface IHandler<TRequest, TResponse>
{
    Task<IResult<TResponse>> HandleAsync(TRequest request);
}
```
