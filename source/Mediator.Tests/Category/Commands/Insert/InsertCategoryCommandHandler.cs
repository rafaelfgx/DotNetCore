using DotNetCore.Results;

namespace DotNetCore.Mediator.Tests;

public sealed class InsertCategoryCommandHandler : IHandler<InsertCategoryCommand, long>
{
    public Task<IResult<long>> HandleAsync(InsertCategoryCommand request)
    {
        return Task.FromResult(1L.Success());
    }
}
