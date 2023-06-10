using System.Text;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
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
            Image = GetFormFile(_firstFormFileContent)
        };
        var courseOperation = await _courseManager.UpsertCourseAsync(courseRequest, user);
        Assert.True(courseOperation.Success);
        
        var getFileOperation =
            await _fileSystemHelper.GetCourseFilesAsync(courseOperation.Value.Id);
        Assert.True(getFileOperation.Success);
        Assert.Single(getFileOperation.Value);

        var courseDto = await _dbContext.CourseDtos.SingleOrDefaultAsync
        (c => c.Id == courseOperation.Value.Id
              && c.Title == courseOperation.Value.Title
              && c.Description == courseRequest.Description
              && c.Contributors.Any(a=>a.UserId == userId));

        Assert.NotNull(courseDto);
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
            Image = GetFormFile(_firstFormFileContent)
        };
        var courseOperation = await _courseManager.UpsertCourseAsync(courseRequest, user);
        Assert.True(courseOperation.Success);
        
        var updateCourseRequest = new UpsertCourseRequest
        {
            CourseId = courseOperation.Value.Id,
            Title = "TestCourseTitleSecond",
            Description = "TestCourseDescriptionSecond",
            Image = GetFormFile(_secondFormFileContent)
        };
        var updateCourseOperation = await _courseManager.UpsertCourseAsync(updateCourseRequest, user);
        Assert.True(updateCourseOperation.Success);
        
        var getFileOperation =
            await _fileSystemHelper.GetCourseFilesAsync(courseOperation.Value.Id);
        Assert.True(getFileOperation.Success);
        Assert.Single(getFileOperation.Value);
        Assert.Equal(_secondFormFileContent, Encoding.UTF8.GetString(getFileOperation.Value.First()));

        var courseDto = await _dbContext.CourseDtos.SingleOrDefaultAsync
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
            Image = GetFormFile(_firstFormFileContent)
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
}