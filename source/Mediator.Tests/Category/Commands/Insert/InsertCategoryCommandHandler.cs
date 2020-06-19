using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator.Tests
{
    public sealed class InsertCategoryCommandHandler : IRequestHandler<InsertCategoryCommand, long>
    {
        public Task<IResult<long>> HandleAsync(InsertCategoryCommand command)
        {
            return Result<long>.SuccessAsync(1L);
        }
    }
}
