using DotNetCore.Results;

namespace DotNetCore.Mediator.Tests;

public sealed class UpdatedCategoryEventHandler : IHandler<UpdatedCategoryEvent>
{
    public Task<IResult> HandleAsync(UpdatedCategoryEvent request)
    {
        return Task.FromResult(Result.Success());
    }
}
