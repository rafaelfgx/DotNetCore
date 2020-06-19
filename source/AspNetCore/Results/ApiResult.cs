using DotNetCore.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCore.AspNetCore
{
    public class ApiResult : IActionResult
    {
        private readonly IResult _result;

        private ApiResult(IResult result)
        {
            _result = result;
        }

        private ApiResult(object data)
        {
            _result = Result<object>.Success(data);
        }

        public static IActionResult Create(IResult result)
        {
            return new ApiResult(result);
        }

        public static IActionResult Create(object data)
        {
            return new ApiResult(data);
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            object value = null;

            if (_result.Failed)
            {
                value = _result.Message;
            }
            else if (_result.GetType().IsGenericType && _result.GetType().GetGenericTypeDefinition() == typeof(Result<>))
            {
                value = (_result as dynamic)?.Data;
            }

            var objectResult = new ObjectResult(value)
            {
                StatusCode = _result.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status422UnprocessableEntity
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}
