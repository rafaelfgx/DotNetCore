using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator.Tests
{
    public sealed class UpdatedCategoryEventHandler : IHandler<UpdatedCategoryEvent>
    {
        public Task<IResult> HandleAsync(UpdatedCategoryEvent request)
        {
            return Task.FromResult(Result.Success());
        }
    }
}
