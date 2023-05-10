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
    private readonly CourseManager _courseManager;
    private readonly FileSystemHelper _fileSystemHelper;
    private readonly ViewManager _viewManager;  
    private readonly UserInfoManager _userInfoManager;      

    public ArticleManager(ArticleRepository articleRepository,
        CourseManager courseManager,
        FileSystemHelper fileSystemHelper,
        ViewManager viewManager,
        UserInfoManager userInfoManager)
    {
        _articleRepository = articleRepository;
        _courseManager = courseManager;
        _fileSystemHelper = fileSystemHelper;
        _viewManager = viewManager;
        _userInfoManager = userInfoManager;
    }

    public async Task<OperationResult<ArticleDto>> UpsertArticleAsync(UpsertArticleRequest request, User user)
    {
        var courseOperation = await _courseManager.GetCourseByIdAsync(request.CourseId);
        if (!courseOperation.Success)
            return new(courseOperation);

        OperationResult<ArticleDto> upsertOperation;
        if (request.ArticleId != null)
            upsertOperation = await _articleRepository.UpdateArticleAsync(request, user);
        else
            upsertOperation = await _articleRepository.InsertArticleAsync(request, user, courseOperation.Value);

        if (!upsertOperation.Success || request.Files == null)
            return upsertOperation;

        await _courseManager.UpsertArticleToCourseAsync(request.CourseId, upsertOperation.Value, user);
        
        
        var deleteOperation = await _fileSystemHelper.DeleteArticleFilesAsync(request.CourseId, upsertOperation.Value.Id);
        if (!deleteOperation.Success)
            return new(deleteOperation);
        
        var saveOperation = await _fileSystemHelper.SaveArticleFilesAsync(request.CourseId, upsertOperation.Value.Id, request.Files);
        if (!saveOperation.Success)
            return new(saveOperation);
            
        return new(upsertOperation);
    }


    public async Task<OperationResult<GetArticleResponse>> GetArticleContentAsync(string coursePath, string articlePath, User user)
    {
        var articleOperation = await _articleRepository.GetArticleByPathAsync($"{coursePath}/{articlePath}");
        if (!articleOperation.Success)
            return new(articleOperation);

        var filesOperation = await
            _fileSystemHelper.GetArticleFilesAsync(articleOperation.Value.Course.Id, articleOperation.Value.Id);
        if (!filesOperation.Success)
            return new(filesOperation);

        await _viewManager.AddViewAsync(articleOperation.Value.Id, user);
        await _userInfoManager.SetLastReadCourseIdAsync(user, articleOperation.Value.Course.Id);

        return new(new GetArticleResponse(articleOperation.Value, filesOperation.Value));
    }


    public async Task<OperationResult<ArticleDto>> DeleteArticleAsync(int articleId)
    {
        var articleOperation = await _articleRepository.DeleteArticleByIdAsync(articleId);
        if (!articleOperation.Success)
            return articleOperation;
        
        var filesOperation =
            await _fileSystemHelper.DeleteArticleFilesAsync(articleOperation.Value.Course.Id, articleOperation.Value.Id);
        if (!filesOperation.Success)
            return new(filesOperation);
        
        return articleOperation;
    }
}