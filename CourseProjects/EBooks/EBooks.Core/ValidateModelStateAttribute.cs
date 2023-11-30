using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EBooks.Core;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        if (actionContext.ModelState.IsValid)
            return;
        actionContext.Result = new JsonResult(actionContext.ModelState)
        {
            StatusCode = (int)HttpStatusCode.BadRequest
        };
        base.OnActionExecuting(actionContext);
    }
}