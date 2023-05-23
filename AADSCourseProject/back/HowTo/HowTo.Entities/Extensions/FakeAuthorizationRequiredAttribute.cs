using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HowTo.Entities.Extensions;

public class FakeAuthorizationRequiredAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        var cookie = actionContext.HttpContext.Request.Cookies[Constants.FakeAuthCookie];
        if (!string.IsNullOrEmpty(cookie))
        {
            var data = cookie.Split(":");
            if (data.Length == 2 && !string.IsNullOrEmpty(data[0]) && !string.IsNullOrEmpty(data[1]) &&
                Guid.TryParse(data[1], out var id))
            {
                var user = new User(id, data[0]);

                actionContext.HttpContext.User = new GenericPrincipal(new UserIdentity(user), Array.Empty<string>());
                return;
            }
        }

        actionContext.Result = new UnauthorizedResult();
        base.OnActionExecuting(actionContext);
    }
}