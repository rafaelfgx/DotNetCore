using FluentValidation;

namespace DotNetCore.Validation;

public static class Extensions
{
    public static Tuple<bool, string> Validation<T>(this IValidator<T> validator, T instance)
    {
        if (instance is null) return new(false, default);

        var result = validator.Validate(instance);

        return new(result.IsValid, result.ToString());
    }
}
