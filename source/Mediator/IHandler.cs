using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator
{
    public interface IHandler<in TRequest>
    {
        Task<IResult> HandleAsync(TRequest request);
    }

    public interface IHandler<in TRequest, TResponse>
    {
        Task<IResult<TResponse>> HandleAsync(TRequest request);
    }
}
