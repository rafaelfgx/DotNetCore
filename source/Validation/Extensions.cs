using DotNetCore.Results;
using FluentValidation;

namespace DotNetCore.Validation;

public static class Extensions
{
    public static IResult Validation<T>(this IValidator<T> validator, T instance)
    {
        if (instance is null) return Result.Fail();

        var result = validator.Validate(instance);

        return result.IsValid ? Result.Success() : Result.Fail(result.ToString());
    }
}
