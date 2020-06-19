using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator.Tests
{
    public sealed class CategoryByIdQueryHandler : IRequestHandler<CategoryByIdQuery, Category>
    {
        public Task<IResult<Category>> HandleAsync(CategoryByIdQuery request)
        {
            return Result<Category>.SuccessAsync(new Category());
        }
    }
}
