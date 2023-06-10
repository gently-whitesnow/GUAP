using System;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.DataAccess.Helpers;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.Article;

namespace HowTo.DataAccess.Managers;

public class ArticleManager
{
    private readonly ArticleRepository _articleRepository;
    private readonly FileSystemHelper _fileSystemHelper;
    private readonly ViewManager _viewManager;
    private readonly UserInfoManager _userInfoManager;

    public ArticleManager(ArticleRepository articleRepository,
        FileSystemHelper fileSystemHelper,
        ViewManager viewManager,
        UserInfoManager userInfoManager)
    {
        _articleRepository = articleRepository;
        _fileSystemHelper = fileSystemHelper;
        _viewManager = viewManager;
        _userInfoManager = userInfoManager;
    }

    public async Task<OperationResult<ArticlePublic>> UpsertArticleAsync(UpsertArticleRequest request, User user)
    {
        var upsertOperation = await _articleRepository.UpsertArticleAsync(request, user);
        if (!upsertOperation.Success)
            return new(upsertOperation);
        
        var userOperation = await _userInfoManager.GetUserInfoAsync(user);
        if (userOperation is { Success: false, ActionStatus: ActionStatus.InternalServerError })
            return new(userOperation);

        if (request.File == null)
            return new(new ArticlePublic(upsertOperation.Value, user, userOperation.Value));

        var deleteOperation =
            await _fileSystemHelper.DeleteArticleDirectoryAsync(request.CourseId, upsertOperation.Value.Id);
        if (!deleteOperation.Success)
            return new(deleteOperation);

        var saveOperation =
            await _fileSystemHelper.SaveArticleFilesAsync(request.CourseId, upsertOperation.Value.Id, request.File);
        if (!saveOperation.Success)
            return new(saveOperation);

        return new(new ArticlePublic(upsertOperation.Value, user, userOperation.Value));
    }


    public async Task<OperationResult<GetArticleResponse>> GetArticleWithFileByIdAsync(
        int courseId,
        int articleId,
        User user)
    {
        var articleOperation = await _articleRepository.GetArticleByIdAsync(courseId, articleId);
        if (!articleOperation.Success)
            return new(articleOperation);

        var filesOperation = await
            _fileSystemHelper.GetArticleFilesAsync(articleOperation.Value.CourseId, articleOperation.Value.Id);
        if (!filesOperation.Success)
            if(filesOperation.ActionStatus == ActionStatus.Ok)
                return new(Errors.ArticleFileNotFound(courseId, articleId));
            else
                return new(filesOperation);
        
        await _viewManager.AddViewAsync(articleOperation.Value.CourseId, articleOperation.Value.Id, user);
        await _userInfoManager.SetLastReadCourseIdAsync(user, articleOperation.Value.CourseId);

        var userOperation = await _userInfoManager.GetUserInfoAsync(user);
        if (userOperation is { Success: false, ActionStatus: ActionStatus.InternalServerError })
            return new(userOperation);
        
        return new(
            new GetArticleResponse(
                new ArticlePublic(articleOperation.Value, user, userOperation.Value),
                filesOperation.Value));
    }


    public async Task<OperationResult<ArticleDto>> DeleteArticleAsync(int courseId, int articleId)
    {
        var articleOperation = await _articleRepository.DeleteArticleByIdAsync(courseId, articleId);
        if (!articleOperation.Success)
            return articleOperation;

        var filesOperation =
            await _fileSystemHelper.DeleteArticleDirectoryAsync(articleOperation.Value.CourseId,
                articleOperation.Value.Id);
        if (!filesOperation.Success)
            return new(filesOperation);

        return articleOperation;
    }
}