using Microsoft.AspNetCore.Mvc;

namespace Flow;

public static class ResultExtensions
{
    public static IActionResult AsActionResult<T>(this Result<T> operation)
    {
        if (operation.IsSuccess)
            return new JsonResult(operation.Value);

        return new JsonResult(operation.Error)
        {
            StatusCode = operation.Error.ExtractStatusCode()
        };
    }
    
    public static IActionResult AsActionResult(this Result operation)
    {
        if (operation.IsSuccess)
            return new OkResult();

        return new JsonResult(operation.Error)
        {
            StatusCode = operation.Error.ExtractStatusCode()
        };
    }
}