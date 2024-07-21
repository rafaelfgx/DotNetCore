# DotNetCore.Validation

## Extensions

```cs
public static class Extensions
{
    public static Tuple<bool, string> Validation<T>(this IValidator<T> validator, T instance) { }
}
```
