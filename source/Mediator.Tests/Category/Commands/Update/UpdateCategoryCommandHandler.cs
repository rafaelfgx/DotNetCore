using DotNetCore.Results;

namespace DotNetCore.Mediator.Tests;

public sealed class UpdateCategoryCommandHandler : IHandler<UpdateCategoryCommand>
{
    private readonly IMediator _mediator;

    public UpdateCategoryCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IResult> HandleAsync(UpdateCategoryCommand request)
    {
        var category = new Category(request.Id, request.Name);

        var updatedCategoryEvent = new UpdatedCategoryEvent(category);

        await _mediator.HandleAsync(updatedCategoryEvent).ConfigureAwait(false);

        return Result.Success();
    }
}
