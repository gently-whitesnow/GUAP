using System.Text;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;
using Microsoft.EntityFrameworkCore;

namespace HowTo.Tests;

public class ArticleTests : BaseTests
{
    public ArticleTests() : base("/Users/gently/Temp/ArticleTests-howto-test-content")
    {
    }
    
    [Fact]
    private async void CreateArticleAsync()
    {
        var userId = Guid.NewGuid();
        var user = new User(userId, "TestUserName");
        var courseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitle",
            Description = "TestCourseDescription"
        };
        var courseOperation = await _courseManager.UpsertCourseAsync(courseRequest, user);
        Assert.True(courseOperation.Success);

        var articleRequest = new UpsertArticleRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        };

        var articleOperation = await _articleManager.UpsertArticleAsync(articleRequest, user);
        Assert.True(articleOperation.Success);

        var getFileOperation =
            await _fileSystemHelper.GetArticleFilesAsync(articleRequest.CourseId, articleOperation.Value.Id);
        Assert.True(getFileOperation.Success);
        Assert.Single(getFileOperation.Value);

        var articleDto = await _dbContext.ArticleDtos.SingleOrDefaultAsync
        (a => a.CourseId == articleRequest.CourseId
              && a.Id == articleOperation.Value.Id
              && a.Title == articleRequest.Title
              && a.Author.UserId == user.Id);

        Assert.NotNull(articleDto);
    }

    [Fact]
    private async void UpdateArticleAsync()
    {
        var userId = Guid.NewGuid();
        var user = new User(userId, "TestUserName");
        var courseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitle",
            Description = "TestCourseDescription"
        };
        var courseOperation = await _courseManager.UpsertCourseAsync(courseRequest, user);
        Assert.True(courseOperation.Success);

        var insertArticleOperation = await _articleManager.UpsertArticleAsync(new UpsertArticleRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        }, user);
        Assert.True(insertArticleOperation.Success);

        var updateArticleRequest = new UpsertArticleRequest
        {
            ArticleId = insertArticleOperation.Value.Id,
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleSecondTitle",
            File = GetFormFile(_secondFormFileContent)
        };
        var updateArticleOperation = await _articleManager.UpsertArticleAsync(updateArticleRequest, user);
        Assert.True(updateArticleOperation.Success);

        var getFileOperation =
            await _fileSystemHelper.GetArticleFilesAsync(courseOperation.Value.Id, updateArticleOperation.Value.Id);
        Assert.True(getFileOperation.Success);
        Assert.Single(getFileOperation.Value);
        Assert.Equal(_secondFormFileContent, Encoding.UTF8.GetString(getFileOperation.Value.First()));

        var articleDto = await _dbContext.ArticleDtos.SingleOrDefaultAsync
        (a => a.CourseId == courseOperation.Value.Id
              && a.Id == insertArticleOperation.Value.Id
              && a.Title == updateArticleRequest.Title);

        Assert.NotNull(articleDto);
    }

    [Fact]
    private async void GetArticleAsync()
    {
        var userId = Guid.NewGuid();
        var user = new User(userId, "TestUserName");
        var courseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitle",
            Description = "TestCourseDescription"
        };
        var courseOperation = await _courseManager.UpsertCourseAsync(courseRequest, user);
        Assert.True(courseOperation.Success, courseOperation.DumpAllErrors());

        var forCheckingLastCourseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitleSecond",
            Description = "TestCourseDescription"
        };
        var forCheckingLastCourseOperation = await _courseManager.UpsertCourseAsync(forCheckingLastCourseRequest, user);
        Assert.True(forCheckingLastCourseOperation.Success, forCheckingLastCourseOperation.DumpAllErrors());

        var articleRequest = new UpsertArticleRequest
        {
            CourseId = forCheckingLastCourseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        };

        var upsertArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, user);
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
        var firstUserId = Guid.NewGuid();
        var secondUserId = Guid.NewGuid();
        var firstUser = new User(firstUserId, "FirstTestUserName");
        var secondUser = new User(secondUserId, "SecondTestUserName");
        var courseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitle",
            Description = "TestCourseDescription"
        };
        var courseOperation = await _courseManager.UpsertCourseAsync(courseRequest, firstUser);
        Assert.True(courseOperation.Success, courseOperation.DumpAllErrors());
        
        var articleRequest = new UpsertArticleRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        };

        var firstArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, firstUser);
        Assert.True(firstArticleOperation.Success, firstArticleOperation.DumpAllErrors());
        var secondArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, secondUser);
        Assert.True(secondArticleOperation.Success, secondArticleOperation.DumpAllErrors());
        
        var firstGetBySecondArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(firstArticleOperation.Value.CourseId,
                firstArticleOperation.Value.Id, secondUser);
        Assert.True(firstGetBySecondArticleOperation.Success, firstGetBySecondArticleOperation.DumpAllErrors());
        
        var secondGetByFirstArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(firstArticleOperation.Value.CourseId,
                firstArticleOperation.Value.Id, firstUser);
        Assert.True(secondGetByFirstArticleOperation.Success, secondGetByFirstArticleOperation.DumpAllErrors());
        
        var deleteOperation = await _courseManager.DeleteCourseAsync(firstArticleOperation.Value.CourseId);
        Assert.True(deleteOperation.Success, deleteOperation.DumpAllErrors());
        
        var getAfterDeleteFirstArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(firstArticleOperation.Value.CourseId,
                firstArticleOperation.Value.Id, secondUser);
        Assert.Equal(ActionStatus.BadRequest, getAfterDeleteFirstArticleOperation.ActionStatus);
        
        var getAfterDeleteSecondArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(firstArticleOperation.Value.CourseId,
                firstArticleOperation.Value.Id, firstUser);
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
        var userId = Guid.NewGuid();
       
        var user = new User(userId, "TestUserName");
        var courseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitle",
            Description = "TestCourseDescription"
        };
        var courseOperation = await _courseManager.UpsertCourseAsync(courseRequest, user);
        Assert.True(courseOperation.Success, courseOperation.DumpAllErrors());
        
        var articleRequest = new UpsertArticleRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestArticleTitle",
            File = GetFormFile(_firstFormFileContent)
        };

        var upsertArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, user);
        Assert.True(upsertArticleOperation.Success, upsertArticleOperation.DumpAllErrors());
        
        var getArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(upsertArticleOperation.Value.CourseId,
                upsertArticleOperation.Value.Id, user);
        Assert.True(getArticleOperation.Success, getArticleOperation.DumpAllErrors());
        
        var deleteOperation = await _articleManager.DeleteArticleAsync(getArticleOperation.Value.Article.CourseId, getArticleOperation.Value.Article.Id);
        Assert.True(deleteOperation.Success, deleteOperation.DumpAllErrors());
        
        var getAfterDeleteSecondArticleOperation =
            await _articleManager.GetArticleWithFileByIdAsync(deleteOperation.Value.CourseId,
                deleteOperation.Value.Id, user);
        Assert.Equal(ActionStatus.BadRequest, getAfterDeleteSecondArticleOperation.ActionStatus);
        
        var getFileAfterDeleteFirstArticleOperation = await _fileSystemHelper.GetArticleFilesAsync(
            deleteOperation.Value.CourseId,
            deleteOperation.Value.Id);
        Assert.Equal(ActionStatus.Ok, getFileAfterDeleteFirstArticleOperation.ActionStatus);
        Assert.False(getFileAfterDeleteFirstArticleOperation.Success);
    }
}