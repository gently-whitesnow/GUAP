using HowTo.Entities;
using HowTo.Entities.Course;

namespace HowTo.Tests;

public class SummaryTests : BaseTests
{
    public SummaryTests() : base("/Users/gently/Temp/SummaryTests-howto-test-content")
    {
    }

    [Fact]
    public async void CheckSummaryFilesAsync()
    {
        var userId = Guid.NewGuid();
        var user = new User(userId, "TestUserName");
        var firstCourseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitleFirst",
            Description = "TestCourseDescription",
            File = GetFormImage()
        };
        var firstCourseOperation = await _courseManager.UpsertCourseAsync(firstCourseRequest, user);
        Assert.True(firstCourseOperation.Success, firstCourseOperation.DumpAllErrors());

        var secondCourseRequest = new UpsertCourseRequest
        {
            Title = "TestCourseTitleSecond",
            Description = "TestCourseDescription",
            File = GetFormImage()
        };
        var secondCourseOperation = await _courseManager.UpsertCourseAsync(secondCourseRequest, user);
        Assert.True(secondCourseOperation.Success, firstCourseOperation.DumpAllErrors());

        var summaryOperation = await _summaryManager.GetSummaryAsync(user);
        Assert.True(summaryOperation.Success, firstCourseOperation.DumpAllErrors());
        Assert.Equal(2, summaryOperation.Value.Courses.Count(d => d.Files.Count() == 1));
    }
}