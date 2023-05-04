using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using HowTo.Entities.Article;
using Microsoft.AspNetCore.Mvc;

namespace HowTo;

public class ArticleController: Controller
{
    private readonly ArticleManager _articleManager;

    public ArticleController(ArticleManager articleManager)
    {
        _articleManager = articleManager;
    }

    /// <summary>
    /// Создание статьи
    /// </summary>
    [HttpPost]
    [Route("api/articles")]
    public Task<IActionResult> CreateArticleAsync(CreateArticleRequest request)
    {
        return _articleManager.CreateArticleAsync(request).AsActionResultAsync();
    }
    
    /// <summary>
    /// Удаление статьи
    /// </summary>
    [HttpDelete]
    [Route("api/articles/{articleId}")]
    public Task<IActionResult> DeleteArticleAsync([FromRoute] Guid articleId)
    {
        return _articleManager.DeleteArticleAsync(articleId).AsActionResultAsync();
    }

    /// <summary>
    /// Получение контента статьи
    /// </summary>
    [HttpPost]
    [Route("api/courses/{coursePath}/{articlePath}")]
    public Task<IActionResult> GetArticleContentAsync([FromRoute] string coursePath, [FromRoute] string articlePath)
    {
        return _articleManager.GetArticleContentAsync(coursePath, articlePath).AsActionResultAsync();
    }
}