# DotNetCore.Mediator

## Tests

 ```csharp
[TestClass]
public class Tests
{
    private readonly IMediator _mediator;

    public Tests()
    {
        var services = new ServiceCollection();

        services.AddMediator(typeof(Tests).Assembly);

        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
    }

    [TestMethod]
    public void CategoryByIdQuery()
    {
        var query = new CategoryByIdQuery { Id = 1L };

        var response = _mediator.HandleAsync<CategoryByIdQuery, Category>(query).Result;

        Assert.IsTrue(response.Succeeded);
        Assert.IsNotNull(response.Data);
    }

    [TestMethod]
    public void InsertCategoryCommand()
    {
        var command = new InsertCategoryCommand { Name = "Name" };

        var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

        Assert.IsTrue(response.Succeeded);
        Assert.IsTrue(response.Data > 0);
    }

    [TestMethod]
    public void InsertCategoryCommand_ShouldFail()
    {
        var command = new InsertCategoryCommand();

        var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

        Assert.IsTrue(response.Failed);
    }

    [TestMethod]
    public void UpdateCategoryCommand()
    {
        var command = new UpdateCategoryCommand { Id = 1, Name = "Name Updated" };

        var response = _mediator.HandleAsync(command).Result;

        Assert.IsTrue(response.Succeeded);
    }
}
 ```

## Query

```cs
public class CategoryByIdQuery : IRequest
{
    public long Id { get; set; }
}
```

## QueryHandler

```cs
public sealed class CategoryByIdQueryHandler : IRequestHandler<CategoryByIdQuery, Category>
{
    public Task<IResult<Category>> HandleAsync(CategoryByIdQuery request)
    {
        return Result<Category>.SuccessAsync(new Category());
    }
}
```

## Command

```cs
public class InsertCategoryCommand : IRequest
{
    public string Name { get; set; }
}
```

## CommandHandler

```cs
public sealed class InsertCategoryCommandHandler : IRequestHandler<InsertCategoryCommand, long>
{
    public Task<IResult<long>> HandleAsync(InsertCategoryCommand command)
    {
        return Result<long>.SuccessAsync(1L);
    }
}
```

## CommandValidator

```cs
public class InsertCategoryCommandValidator : AbstractValidator<InsertCategoryCommand>
{
    public InsertCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
```

## Command

```cs
public class UpdateCategoryCommand : IRequest
{
    public long Id { get; set; }

    public string Name { get; set; }
}
```

## CommandHandler

```cs
public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    public Task<IResult> HandleAsync(UpdateCategoryCommand request)
    {
        return Result.SuccessAsync();
    }
}
```

## Mediator

```cs
public interface IMediator
{
    Task HandleAsync<TRequest>(TRequest request) where TRequest : IRequest;

    Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest;
}
```

```cs
public sealed class Mediator : IMediator
{
    public Task HandleAsync<TRequest>(TRequest request) where TRequest : IRequest { }

    public Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest { }
}
```

```cs
public interface IRequest { }
```

```cs
public interface IRequestHandler<in TRequest> where TRequest : IRequest
{
    Task HandleAsync(TRequest request);
}

public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest
{
    Task<TResponse> HandleAsync(TRequest request);
}
```
