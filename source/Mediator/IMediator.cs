using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator
{
    public interface IMediator
    {
        Task<IResult> HandleAsync<TRequest>(TRequest request);

        Task<IResult<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request);
    }
}
