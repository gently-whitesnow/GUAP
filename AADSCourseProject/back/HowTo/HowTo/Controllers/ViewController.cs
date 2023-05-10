using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using HowTo.Entities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HowTo.Controllers;

[FakeAuthorizationRequired]
public class ViewController : Controller
{
    private readonly UserInfoManager _userInfoManager;

    public ViewController(UserInfoManager userInfoManager)
    {
        _userInfoManager = userInfoManager;
    }

    /// <summary>
    /// Добавлени подтвержденного просмотра
    /// </summary>
    [HttpPost]
    [Route("api/views/approved")]
    [ValidateModelState]
    public Task<IActionResult> CreateCourseAsync([FromBody][Required] int articleId)
    {
        var user = HttpContext.GetUser();
        return _userInfoManager.AddApprovedViewAsync(user, articleId).AsActionResultAsync();
    }
}