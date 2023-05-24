using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using HowTo.Entities.Extensions;
using HowTo.Entities.ViewedEntity;
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
    /// Добавление подтвержденного просмотра на странице статьи
    /// </summary>
    [HttpPost]
    [Route("api/views/approved")]
    [ValidateModelState]
    public Task<IActionResult> AddApprovedViewAsync([FromBody][Required] AddApprovedViewRequest request)
    {
        var user = HttpContext.GetUser();
        return _userInfoManager.AddApprovedViewAsync(user, request).AsActionResultAsync();
    }
}