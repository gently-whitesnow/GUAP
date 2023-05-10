using Microsoft.AspNetCore.Http;

namespace HowTo.Entities.Extensions;

public static class HttpContextExtension
{
    public static User GetUser(this HttpContext requestContext)
    {
        var identity = requestContext.User.Identity as UserIdentity;
        return identity?.User;
    }
}