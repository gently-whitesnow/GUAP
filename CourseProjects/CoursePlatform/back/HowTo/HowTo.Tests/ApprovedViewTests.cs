using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;
using HowTo.Entities.ViewedEntity;

namespace HowTo.Tests;

public class ApprovedViewTests : BaseTests
{
    public ApprovedViewTests() : base("/Users/gently/Temp/ApprovedViewTests-howto-test-content")
    {
    }
    
    [Fact]
    public async void AddApprovedViewAsync()
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

        var firstArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, user);
        Assert.True(firstArticleOperation.Success);
        
        var secondArticleOperation = await _articleManager.UpsertArticleAsync(articleRequest, user);
        Assert.True(secondArticleOperation.Success);

        var approvedViewOperation = await _userInfoManager.AddApprovedViewAsync(user, new AddApprovedViewRequest
        {
            CourseId = firstArticleOperation.Value.CourseId,
            ArticleId = firstArticleOperation.Value.Id
        });
        Assert.True(approvedViewOperation.Success);

        var summaryOperation = await _summaryManager.GetSummaryAsync(user);
        Assert.True(summaryOperation.Success);
        Assert.Equal(1, summaryOperation.Value.LastCourse?.UserApprovedViews);
        
        var getArticleOperation = await _articleManager.GetArticleWithFileByIdAsync(
            firstArticleOperation.Value.CourseId,firstArticleOperation.Value.Id, user);
        Assert.True(getArticleOperation.Success, getArticleOperation.DumpAllErrors());
        
        Assert.True(getArticleOperation.Value.Article.IsAuthor);
        Assert.True(getArticleOperation.Value.Article.IsViewed);
    }
}