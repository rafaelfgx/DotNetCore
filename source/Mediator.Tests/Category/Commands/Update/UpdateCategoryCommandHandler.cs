using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator.Tests
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        public Task<IResult> HandleAsync(UpdateCategoryCommand request)
        {
            return Result.SuccessAsync();
        }
    }
}
