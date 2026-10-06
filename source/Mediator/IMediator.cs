using DotNetCore.Results;

namespace DotNetCore.Mediator;

public interface IMediator
{
    Task<Result> HandleAsync<TRequest>(TRequest request);

    Task<Result<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request);
}
