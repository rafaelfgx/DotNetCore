namespace DotNetCore.Results;

public static class Extensions
{
    public static IResult<T> Fail<T>(this IResult result)
    {
        return Result<T>.Fail(result.Message);
    }

    public static IResult<T> Success<T>(this T data)
    {
        return Result<T>.Success(data);
    }

    public static IResult<T> Success<T>(this T data, string message)
    {
        return Result<T>.Success(data, message);
    }
}
