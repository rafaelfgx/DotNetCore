using DotNetCore.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCore.AspNetCore
{
    public sealed class ApiResult : IActionResult
    {
        private readonly IResult _result;

        public ApiResult(IResult result)
        {
            _result = result;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            if (_result.Failed) return context.UnprocessableEntity(_result.Message);

            return context.Ok(_result.Message);
        }
    }

    public sealed class ApiResult<T> : IActionResult
    {
        private readonly IResult<T> _result;

        public ApiResult(IResult<T> result)
        {
            _result = result;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            if (_result.Failed) return context.UnprocessableEntity(_result.Message);

            return context.Ok(_result.Data);
        }
    }
}
