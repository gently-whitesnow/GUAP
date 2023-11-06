using System.Text;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;
using Microsoft.EntityFrameworkCore;

namespace HowTo.Tests;

public class ArticleTests : BaseTestsWithArtefacts
{
    public ArticleTests() : base("/Users/gently/Temp/ArticleTests-howto-test-content")
    {
    }
    
    [Fact]
    private async void CreateArticleAsync()
    {
        var courseOperation = await InitCourseAsync(user: FirstUser);

        var articleRequest = new UpsertArticleRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        };

        var articleOperation = await _articleManager.UpsertArticleAsync(articleRequest, FirstUser);
        Assert.True(articleOperation.Success);

        var getFileOperation =
            await _fileSystemHelper.GetArticleFilesAsync(articleRequest.CourseId, articleOperation.Value.Id);
        Assert.True(getFileOperation.Success);
        Assert.Single(getFileOperation.Value);

        var articleDto = await _dbContext.ArticleContext.SingleOrDefaultAsync
        (a => a.CourseId == articleRequest.CourseId
              && a.Id == articleOperation.Value.Id
              && a.Title == articleRequest.Title
              && a.Author.UserId == FirstUser.Id);

        Assert.NotNull(articleDto);
    }

    [Fact]
    private async void UpdateArticleAsync()
    {
        var courseOperation = await InitCourseAsync(user: FirstUser);

        var insertArticleOperation = await _articleManager.UpsertArticleAsync(new UpsertArticleRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        }, FirstUser);
        Assert.True(insertArticleOperation.Success);

        var updateArticleRequest = new UpsertArticleRequest
        {
            ArticleId = insertArticleOperation.Value.Id,
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleSecondTitle",
            File = GetFormFile(_secondFormFileContent)
        };
        var updateArticleOperation = await _articleManager.UpsertArticleAsync(updateArticleRequest, FirstUser);
        Assert.True(updateArticleOperation.Success);

        var getFileOperation =
            await _fileSystemHelper.GetArticleFilesAsync(courseOperation.Value.Id, updateArticleOperation.Value.Id);
        Assert.True(getFileOperation.Success);
        Assert.Single(getFileOperation.Value);
        Assert.Equal(_secondFormFileContent, Encoding.UTF8.GetString(getFileOperation.Value.First()));

        var articleDto = await _dbContext.ArticleContext.SingleOrDefaultAsync
        (a => a.CourseId == courseOperation.Value.Id
              && a.Id == insertArticleOperation.Value.Id
              && a.Title == updateArticleRequest.Title);

        Assert.NotNull(articleDto);
    }

    [Fact]
    private async void GetArticleAsync()
    {
        await InitCourseAsync(user: FirstUser, courseTitle: "FirstCourseTitle");
        
        var forCheckingLastCourseOperation = await InitCourseAsync(user: FirstUser, courseTitle: "SecondCourseTitle");

        var articleRequest = new UpsertArticleRequest
        {
            CourseId = forCheckingLastCourseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        };

        var upsertArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, FirstUser);
        Assert.True(upsertArticleOperation.Success, upsertArticleOperation.DumpAllErrors());
        var requesterUserId = Guid.NewGuid();
        var requesterUser = new User(requesterUserId, "RequesterTestUserName");
        var getArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(upsertArticleOperation.Value.CourseId,
                upsertArticleOperation.Value.Id, requesterUser);
        Assert.True(getArticleOperation.Success, getArticleOperation.DumpAllErrors());
        var summaryOperation = await _summaryManager.GetSummaryAsync(requesterUser);
        Assert.True(summaryOperation.Success, summaryOperation.DumpAllErrors());

        Assert.Equal(upsertArticleOperation.Value.CourseId, summaryOperation.Value.LastCourse?.Id);
    }

    [Fact]
    private async void DeleteArticleAfterDeleteCourseAsync()
    {
        var courseOperation = await InitCourseAsync(user: FirstUser);
        
        var articleRequest = new UpsertArticleRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        };

        var firstArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, FirstUser);
        Assert.True(firstArticleOperation.Success, firstArticleOperation.DumpAllErrors());
        var secondArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, SecondUser);
        Assert.True(secondArticleOperation.Success, secondArticleOperation.DumpAllErrors());
        
        var firstGetBySecondArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(firstArticleOperation.Value.CourseId,
                firstArticleOperation.Value.Id, SecondUser);
        Assert.True(firstGetBySecondArticleOperation.Success, firstGetBySecondArticleOperation.DumpAllErrors());
        
        var secondGetByFirstArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(firstArticleOperation.Value.CourseId,
                firstArticleOperation.Value.Id, FirstUser);
        Assert.True(secondGetByFirstArticleOperation.Success, secondGetByFirstArticleOperation.DumpAllErrors());
        
        var deleteOperation = await _courseManager.DeleteCourseAsync(firstArticleOperation.Value.CourseId);
        Assert.True(deleteOperation.Success, deleteOperation.DumpAllErrors());
        
        var getAfterDeleteFirstArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(firstArticleOperation.Value.CourseId,
                firstArticleOperation.Value.Id, SecondUser);
        Assert.Equal(ActionStatus.BadRequest, getAfterDeleteFirstArticleOperation.ActionStatus);
        
        var getAfterDeleteSecondArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(firstArticleOperation.Value.CourseId,
                firstArticleOperation.Value.Id, FirstUser);
        Assert.Equal(ActionStatus.BadRequest, getAfterDeleteSecondArticleOperation.ActionStatus);

        var getFileAfterDeleteFirstArticleOperation = await _fileSystemHelper.GetArticleFilesAsync(
            firstArticleOperation.Value.CourseId,
            firstArticleOperation.Value.Id);
        Assert.Equal(ActionStatus.Ok, getFileAfterDeleteFirstArticleOperation.ActionStatus);
        Assert.False(getFileAfterDeleteFirstArticleOperation.Success);
        
        var getFileAfterDeleteSecondArticleOperation = await _fileSystemHelper.GetArticleFilesAsync(
            secondArticleOperation.Value.CourseId,
            secondArticleOperation.Value.Id);
        Assert.Equal(ActionStatus.Ok, getFileAfterDeleteSecondArticleOperation.ActionStatus);
        Assert.False(getFileAfterDeleteSecondArticleOperation.Success);
    }
    
    
    [Fact]
    private async void DeleteArticleAsync()
    {
        var courseOperation = await InitCourseAsync(user: FirstUser);
        
        var articleRequest = new UpsertArticleRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        };

        var upsertArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, FirstUser);
        Assert.True(upsertArticleOperation.Success, upsertArticleOperation.DumpAllErrors());
        
        var getArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(upsertArticleOperation.Value.CourseId,
                upsertArticleOperation.Value.Id, FirstUser);
        Assert.True(getArticleOperation.Success, getArticleOperation.DumpAllErrors());
        
        var deleteOperation = await _articleManager.DeleteArticleAsync(getArticleOperation.Value.Article.CourseId, getArticleOperation.Value.Article.Id);
        Assert.True(deleteOperation.Success, deleteOperation.DumpAllErrors());
        
        var getAfterDeleteSecondArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(deleteOperation.Value.CourseId,
                deleteOperation.Value.Id, FirstUser);
        Assert.Equal(ActionStatus.BadRequest, getAfterDeleteSecondArticleOperation.ActionStatus);
        
        var getFileAfterDeleteFirstArticleOperation = await _fileSystemHelper.GetArticleFilesAsync(
            deleteOperation.Value.CourseId,
            deleteOperation.Value.Id);
        Assert.Equal(ActionStatus.Ok, getFileAfterDeleteFirstArticleOperation.ActionStatus);
        Assert.False(getFileAfterDeleteFirstArticleOperation.Success);
    }
}