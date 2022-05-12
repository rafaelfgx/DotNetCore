using DotNetCore.Results;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.AspNetCore;

public static class ResultExtensions
{
    public static IActionResult ApiResult<T>(this IResult<T> result)
    {
        return new ApiResult<T>(result);
    }

    public static IActionResult ApiResult<T>(this Task<IResult<T>> result)
    {
        return new ApiResult<T>(result.Result);
    }

    public static IActionResult ApiResult(this IResult result)
    {
        return new ApiResult(result);
    }

    public static IActionResult ApiResult(this Task<IResult> result)
    {
        return new ApiResult(result.Result);
    }

    public static IActionResult ApiResult<T>(this T result)
    {
        return ApiResult(Result<T>.Success(result));
    }

    public static IActionResult ApiResult<T>(this Task<T> result)
    {
        return ApiResult(result.Result);
    }
}
