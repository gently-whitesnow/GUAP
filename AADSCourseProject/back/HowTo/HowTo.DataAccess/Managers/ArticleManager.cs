using ATI.Services.Common.Behaviors;
using HowTo.Entities.Article;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HowTo.DataAccess.Managers;

public class ArticleManager
{
    public async Task<OperationResult> CreateArticleAsync(CreateArticleRequest request)
    {
        return new (ActionStatus.Ok);
    }
    
    
    public async Task<OperationResult> GetArticleContentAsync(string coursePath, string articlePath)
    {
        return new (ActionStatus.Ok);
    }
    
    
    public async Task<OperationResult> DeleteArticleAsync(Guid articleId)
    {
        return new (ActionStatus.Ok);
    }
}