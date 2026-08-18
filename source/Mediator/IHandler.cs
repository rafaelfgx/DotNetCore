using DotNetCore.Results;

namespace DotNetCore.Mediator;

public interface IHandler<in TRequest>
{
    Task<Result> HandleAsync(TRequest request);
}

public interface IHandler<in TRequest, TResponse>
{
    Task<Result<TResponse>> HandleAsync(TRequest request);
}
