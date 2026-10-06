using DotNetCore.Results;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.AspNetCore;

public static class ResultExtensions
{
    public static IActionResult ApiResult<T>(this Result<T> result) => new ObjectResult(result.HasMessage ? result.Message : result.Value) { StatusCode = (int)result.Status };

    public static IActionResult ApiResult<T>(this Task<Result<T>> result) => ApiResult(result.Result);

    public static IActionResult ApiResult(this Result result) => new ObjectResult(result.Message) { StatusCode = (int)result.Status };

    public static IActionResult ApiResult(this Task<Result> result) => ApiResult(result.Result);

    public static IActionResult ApiResult<T>(this T result) => new ObjectResult(result);

    public static IActionResult ApiResult<T>(this Task<T> result) => ApiResult(result.Result);
}
