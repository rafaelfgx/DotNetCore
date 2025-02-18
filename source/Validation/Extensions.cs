using FluentValidation;

namespace DotNetCore.Validation;

public static class Extensions
{
    public static Tuple<bool, string> Validation<T>(this IValidator<T> validator, T instance)
    {
        if (instance is null) return new Tuple<bool, string>(false, default);

        var result = validator.Validate(instance);

        return new Tuple<bool, string>(result.IsValid, result.ToString());
    }
}
