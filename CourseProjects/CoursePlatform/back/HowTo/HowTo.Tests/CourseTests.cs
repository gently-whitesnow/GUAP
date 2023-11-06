using System.Text;
using ATI.Services.Common.Behaviors;
using ATI.Services.Common.Extensions.OperationResult;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;
using Microsoft.EntityFrameworkCore;

namespace HowTo.Tests;

public class CourseTests : BaseTests
{
    public CourseTests() : base("/Users/gently/Temp/CourseTests-howto-test-content")
    {
    }

    [Fact]
    private async void CreateCourseAsync()
    {
        var userId = Guid.NewGuid();
        var user = new User(userId, "TestUserName");
        var courseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitle",
            Description = "TestCourseDescription",
            File = GetFormImage()
        };
        await _courseManager.UpsertCourseAsync(courseRequest, user)
            .InvokeOnSuccessAsync(course => _courseManager.GetCourseWithFilesByIdAsync(course.Id, user)
                .InvokeOnSuccessAsync(coursePublic =>
                {
                    Assert.Equal(courseRequest.Title, coursePublic.Title);
                    Assert.Equal(courseRequest.Description, coursePublic.Description);
                    Assert.Single(coursePublic.Files);
                })
                .InvokeOnErrorAsync(operation => Assert.Fail(operation.DumpAllErrors()))
            )
            .InvokeOnErrorAsync(operation => Assert.Fail(operation.DumpAllErrors()));
    }

    [Fact]
    private async void UpdateCourseAsync()
    {
        var userId = Guid.NewGuid();
        var user = new User(userId, "TestUserName");
        var courseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitle",
            Description = "TestCourseDescription",
            File = GetFormImage()
        };
        var courseOperation = await _courseManager.UpsertCourseAsync(courseRequest, user);
        Assert.True(courseOperation.Success);

        var image = GetFormImage();
        var updateCourseRequest = new UpsertCourseRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestCourseTitleSecond",
            Description = "TestCourseDescriptionSecond",
            File = image
        };
        var updateCourseOperation = await _courseManager.UpsertCourseAsync(updateCourseRequest, user);
        Assert.True(updateCourseOperation.Success);

        var getFileOperation =
            await _fileSystemHelper.GetCourseFilesAsync(courseOperation.Value.Id);
        Assert.True(getFileOperation.Success);
        Assert.Single(getFileOperation.Value);
        Assert.False(CompareByteArrayAndFormFile(getFileOperation.Value.First(), image));

        var courseDto = await _dbContext.CourseContext.SingleOrDefaultAsync
        (c => c.Id == updateCourseOperation.Value.Id
              && c.Title == updateCourseRequest.Title
              && c.Description == updateCourseRequest.Description);

        Assert.NotNull(courseDto);
    }

    [Fact]
    private async void DeleteCourseAsync()
    {
        var userId = Guid.NewGuid();

        var user = new User(userId, "TestUserName");
        var courseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitle",
            Description = "TestCourseDescription",
            File = GetFormImage()
        };
        var upsertCourseOperation = await _courseManager.UpsertCourseAsync(courseRequest, user);
        Assert.True(upsertCourseOperation.Success, upsertCourseOperation.DumpAllErrors());

        var getCourseOperation = await _courseManager.GetCourseWithFilesByIdAsync(upsertCourseOperation.Value.Id, user);
        Assert.True(getCourseOperation.Success, getCourseOperation.DumpAllErrors());

        var deleteOperation = await _courseManager.DeleteCourseAsync(getCourseOperation.Value.Id);
        Assert.True(deleteOperation.Success, deleteOperation.DumpAllErrors());

        var getFileAfterDeleteOperation = await _fileSystemHelper.GetCourseFilesAsync(
            deleteOperation.Value.Id);
        Assert.Equal(ActionStatus.Ok, getFileAfterDeleteOperation.ActionStatus);
        Assert.False(getFileAfterDeleteOperation.Success);
    }

    [Fact]
    private async void CheckCourseAuthorsAsync()
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

        var getCourseOperation = await _courseManager.GetCourseWithFilesByIdAsync(courseOperation.Value.Id, firstUser);
        Assert.True(getCourseOperation.Success, secondArticleOperation.DumpAllErrors());
        Assert.Equal(2, getCourseOperation.Value.Contributors.Count());
    }
    
    
}