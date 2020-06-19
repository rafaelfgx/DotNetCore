using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator
{
    public interface IRequestHandler<in TRequest> where TRequest : IRequest
    {
        Task<IResult> HandleAsync(TRequest request);
    }

    public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest
    {
        Task<IResult<TResponse>> HandleAsync(TRequest request);
    }
}
