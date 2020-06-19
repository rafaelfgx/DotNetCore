using DotNetCore.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCore.AspNetCore
{
    public static class ResultExtensions
    {
        public static async Task<IActionResult> ResultAsync<T>(this Task<IResult<T>> result)
        {
            return ApiResult.Create(await result.ConfigureAwait(false));
        }

        public static async Task<IActionResult> ResultAsync(this Task<IResult> result)
        {
            return ApiResult.Create(await result.ConfigureAwait(false));
        }

        public static async Task<IActionResult> ResultAsync<T>(this Task<T> result)
        {
            return ApiResult.Create(await result.ConfigureAwait(false));
        }
    }
}
