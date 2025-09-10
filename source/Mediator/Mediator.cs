using DotNetCore.Results;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace DotNetCore.Mediator;

public sealed class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<Result> HandleAsync<TRequest>(TRequest request)
    {
        var (valid, message) = Validate(request);

        if (!valid) return new Result(HttpStatusCode.BadRequest, message);

        var handler = serviceProvider.GetRequiredService<IHandler<TRequest>>();

        return await handler.HandleAsync(request).ConfigureAwait(false);
    }

    public async Task<Result<TResponse>> HandleAsync<TRequest, TResponse>(TRequest request)
    {
        var (valid, message) = Validate(request);

        if (!valid) return new Result<TResponse>(HttpStatusCode.BadRequest, message);

        var handler = serviceProvider.GetRequiredService<IHandler<TRequest, TResponse>>();

        return await handler.HandleAsync(request).ConfigureAwait(false);
    }

    private (bool, string) Validate<TRequest>(TRequest request)
    {
        var validator = serviceProvider.GetService<AbstractValidator<TRequest>>();

        if (validator is null) return (true, default);

        var validation = validator.Validate(request);

        return (validation.IsValid, validation.ToString());
    }
}
