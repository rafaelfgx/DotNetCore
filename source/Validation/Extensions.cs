using FluentValidation;

namespace DotNetCore.Validation;

public static class Extensions
{
    public static (bool, string) Validation<T>(this IValidator<T> validator, T instance)
    {
        if (instance is null) return (false, null);

        var result = validator.Validate(instance);

        return (result.IsValid, result.ToString());
    }
}
