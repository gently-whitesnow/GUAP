using ATI.Services.Common.Behaviors;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HowTo.Entities.Extensions;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        if (actionContext.ModelState.IsValid)
            return;
        actionContext.Result = CommonBehavior.GetActionResult(ActionStatus.BadRequest, actionContext.ModelState);
        base.OnActionExecuting(actionContext);
    }
}