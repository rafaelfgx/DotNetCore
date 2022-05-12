using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.AspNetCore;

public static class ActionContextExtensions
{
    public static Task Ok(this ActionContext context, object value)
    {
        return new OkObjectResult(value).ExecuteResultAsync(context);
    }

    public static Task UnprocessableEntity(this ActionContext context, string error)
    {
        return new UnprocessableEntityObjectResult(error).ExecuteResultAsync(context);
    }
}
