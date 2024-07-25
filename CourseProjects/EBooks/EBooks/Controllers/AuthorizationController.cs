using System.ComponentModel.DataAnnotations;
using EBooks.Authorization;
using EBooks.Core;
using Microsoft.AspNetCore.Mvc;
using Constants = EBooks.Authorization.Constants;

namespace EBooks.Controllers;

public class FakeAuthorizationController : Controller
{
    /// <summary>
    /// Временная авторизация для тестирования сервиса
    /// </summary>
    [HttpGet]
    [Route("fakeauth")]
    [ValidateModelState]
    public IActionResult GetAuth([Required] [FromQuery] uint userId,
        [Required] [FromQuery] string userName, [FromQuery] string userRole)
    {
        Enum.TryParse<UserRole>(userRole, out var role);
        HttpContext.Response.Cookies.Append(Constants.FakeAuthCookie, $"{userName}:{userId}:{role}",
            new CookieOptions
            {
                Domain = HttpContext.Request.Host.Host,
                Expires = DateTimeOffset.Now.AddDays(10)
            });
        
        return Ok(new User(userId, userName, role));
    }
    
    [HttpGet]
    [Route("auth")]
    [FakeAuthorizationRequired]
    public IActionResult GetAuth()
    {
        return Ok(HttpContext.GetUser());
    }
}