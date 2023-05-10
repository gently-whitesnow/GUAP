using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HowTo.Controllers;

public class FakeAuthorizationController : Controller
{
    /// <summary>
    /// Временная авторизация для тестирования сервиса
    /// </summary>
    [HttpGet]
    [Route("api/fakeauth")]
    public Task<IActionResult> GetAuthAsync([FromQuery] Guid userId, [FromQuery] string userName)
    {
        HttpContext.Response.Cookies.Append(Constants.FakeAuthCookie, $"{userId}:{userName}",
            new CookieOptions
            {
                Domain = HttpContext.Request.Host.Host,
                Expires = DateTimeOffset.FromUnixTimeSeconds(TimeSpan.FromDays(5).Seconds)
            });
        return Task.FromResult<IActionResult>(Ok());
    }
}