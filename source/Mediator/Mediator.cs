using DotNetCore.Results;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.Mediator;

public sealed class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<IResult> HandleAsync<TRequest>(TRequest request)
    {
        var validation = Validate(request);

        if (validation.Failed) return validation;

        var handler = _serviceProvider.GetRequiredService<IHandler<TRequest>>();

        return await handler.HandleAsync(request).ConfigureAwait(false);
    }

    public async Task<IResult<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request)
    {
        var validation = Validate(request);

        if (validation.Failed) return validation.Fail<TResponse>();

        var handler = _serviceProvider.GetRequiredService<IHandler<TRequest, TResponse>>();

        return await handler.HandleAsync(request).ConfigureAwait(false);
    }

    private IResult Validate<TRequest>(TRequest request)
    {
        var validator = _serviceProvider.GetService<AbstractValidator<TRequest>>();

        if (validator is null) return Result.Success();

        var validation = validator.Validate(request);

        return validation.IsValid ? Result.Success() : Result.Fail(validation.ToString());
    }
}
