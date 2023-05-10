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
    public Task<IActionResult> UpsertArticleAsync(UpsertArticleRequest request)
    {
        var user = HttpContext.GetUser();
        return _articleManager.UpsertArticleAsync(request, user).AsActionResultAsync();
    }
    
    /// <summary>
    /// Удаление статьи
    /// </summary>
    [HttpDelete]
    [Route("api/articles/{articleId}")]
    public Task<IActionResult> DeleteArticleAsync([FromRoute] int articleId)
    {
        return _articleManager.DeleteArticleAsync(articleId).AsActionResultAsync();
    }

    /// <summary>
    /// Получение контента статьи
    /// </summary>
    [HttpPost]
    [Route("api/articles/{coursePath}/{articlePath}")]
    public Task<IActionResult> GetArticleContentAsync([FromRoute] string coursePath, [FromRoute] string articlePath)
    {
        var user = HttpContext.GetUser();
        return _articleManager.GetArticleContentAsync(coursePath, articlePath, user).AsActionResultAsync();
    }
}