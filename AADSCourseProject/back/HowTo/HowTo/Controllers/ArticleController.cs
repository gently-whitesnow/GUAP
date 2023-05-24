using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using HowTo.Entities.Article;
using HowTo.Entities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HowTo.Controllers;

[FakeAuthorizationRequired]
public class ArticleController: Controller
{
    private readonly ArticleManager _articleManager;

    public ArticleController(ArticleManager articleManager)
    {
        _articleManager = articleManager;
    }

    /// <summary>
    /// Добавление/обновление статьи
    /// </summary>
    [HttpPost]
    [Route("api/articles")]
    [ValidateModelState]
    public Task<IActionResult> UpsertArticleAsync([FromForm] UpsertArticleRequest request)
    {
        var user = HttpContext.GetUser();
        return _articleManager.UpsertArticleAsync(request, user).AsActionResultAsync();
    }
    
    [HttpDelete]
    [Route("api/articles/{courseId}/{articleId}")]
    [ValidateModelState]
    public Task<IActionResult> DeleteArticleAsync([Required][FromRoute] int courseId,
        [Required][FromRoute] int articleId)
    {
        return _articleManager.DeleteArticleAsync(courseId, articleId).AsActionResultAsync();
    }
    
    [HttpGet]
    [Route("api/articles/{courseId}/{articleId}")]
    [ValidateModelState]
    public Task<IActionResult> GetArticleContentAsync([Required][FromRoute] int courseId, [Required][FromRoute] int articleId)
    {
        var user = HttpContext.GetUser();
        return _articleManager.GetArticleWithFileByIdAsync(courseId, articleId, user).AsActionResultAsync();
    }
}