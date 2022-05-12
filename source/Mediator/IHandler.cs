using DotNetCore.Results;

namespace DotNetCore.Mediator;

public interface IHandler<in TRequest>
{
    Task<IResult> HandleAsync(TRequest request);
}

public interface IHandler<in TRequest, TResponse>
{
    Task<IResult<TResponse>> HandleAsync(TRequest request);
}
