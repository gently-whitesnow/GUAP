using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using HowTo.Entities.Extensions;
using HowTo.Entities.Interactive;
using Microsoft.AspNetCore.Mvc;

namespace HowTo.Controllers;

public class InteractiveController : Controller
{
    private readonly InteractiveManager _interactiveManager;

    public InteractiveController(InteractiveManager interactiveManager)
    {
        _interactiveManager = interactiveManager;
    }

    /// <summary>
    /// Добавление/обновление интерактива
    /// </summary>
    [HttpPost]
    [Route("api/interactive")]
    [ValidateModelState]
    public Task<IActionResult> UpsertInteractiveAsync([FromBody] UpsertInteractiveRequest request)
    {
        return _interactiveManager.UpsertInteractiveAsync(request).AsActionResultAsync();
    }
    
    /// <summary>
    /// Добавление/обновление ответа пользователя
    /// </summary>
    [HttpPost]
    [Route("api/interactive/reply")]
    [ValidateModelState]
    public Task<IActionResult> UpsertInteractiveReplyAsync([FromBody] UpsertInteractiveReplyRequest request)
    {
        var user = HttpContext.GetUser();
        return _interactiveManager.UpsertInteractiveReplyAsync(request, user).AsActionResultAsync();
    }

    [HttpDelete]
    [Route("api/interactive/{interactive}/{interactiveId}")]
    [ValidateModelState]
    public Task<IActionResult> DeleteInteractiveAsync([Required] [FromRoute] Interactive interactive,
        [Required] [FromRoute] int interactiveId)
    {
        return _interactiveManager.DeleteInteractiveAsync(interactive, interactiveId).AsActionResultAsync();
    }

    [HttpGet]
    [Route("api/articles/{courseId}/{articleId}")]
    [ValidateModelState]
    public Task<IActionResult> GetInteractiveContentAsync([Required] [FromRoute] int courseId,
        [Required] [FromRoute] int articleId)
    {
        var user = HttpContext.GetUser();
        return _interactiveManager.GetInteractiveAsync(courseId, articleId, user).AsActionResultAsync();
    }
}