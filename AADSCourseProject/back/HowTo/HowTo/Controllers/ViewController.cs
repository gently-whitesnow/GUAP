using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HowTo.Controllers;

public class ViewController : Controller
{
    private readonly ViewManager _viewManager;

    public ViewController(ViewManager viewManager)
    {
        _viewManager = viewManager;
    }

    /// <summary>
    /// Добавлени подтвержденного просмотра
    /// </summary>
    [HttpPost]
    [Route("api/views/approved")]
    public Task<IActionResult> CreateCourseAsync(Guid articleId)
    {
        return _viewManager.AddApprovedViewAsync(articleId).AsActionResultAsync();
    }
}