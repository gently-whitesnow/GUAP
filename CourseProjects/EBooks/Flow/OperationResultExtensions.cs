using System.Net;
using Flow.StandardOperationError;
using Microsoft.AspNetCore.Mvc;

namespace Flow;

public static class OperationResultExtensions
{
    public static IActionResult AsActionResult<T>(this Operation<T> operation)
    {
        if (operation.IsSuccess)
            return new JsonResult(operation.Value);

        return new JsonResult(operation.Error)
        {
            StatusCode = (int)(operation.Error switch 
            {
                BadRequestOperationError => HttpStatusCode.BadRequest,
                ForbiddenOperationError => HttpStatusCode.Forbidden,
                NotFoundOperationError => HttpStatusCode.NotFound,
                _ => throw new NotImplementedException("Данный тип ошибки не определен")
            })
        };
    }
}