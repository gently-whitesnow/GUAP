using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.DataAccess.Helpers;
using HowTo.Entities;
using HowTo.Entities.Course;
using HowTo.Entities.Summary;

namespace HowTo.DataAccess.Managers;

public class SummaryManager
{
    private readonly UserInfoManager _userInfoManager;
    private readonly CourseManager _courseManager;
    private readonly FileSystemHelper _fileSystemHelper;

    public SummaryManager(UserInfoManager userInfoManager,
        CourseManager courseManager,
        FileSystemHelper fileSystemHelper)
    {
        _userInfoManager = userInfoManager;
        _courseManager = courseManager;
        _fileSystemHelper = fileSystemHelper;
    }

    public async Task<OperationResult<SummaryResponse>> GetSummaryAsync(User user)
    {
        var userOperation = await _userInfoManager.GetUserInfoAsync(user);
        if (userOperation is { Success: false, ActionStatus: ActionStatus.InternalServerError })
            return new(userOperation);

        var allCoursesOperation = await _courseManager.GetAllCoursesAsync();
        if (allCoursesOperation is { Success: false, ActionStatus: ActionStatus.InternalServerError })
            return new(allCoursesOperation);

        var lastCourse = allCoursesOperation.Value.FirstOrDefault(c => c.Id == userOperation.Value?.LastReadCourseId) ??
                         allCoursesOperation.Value.FirstOrDefault();
        if (lastCourse == null)
            return new OperationResult<SummaryResponse>();
        var summaryResponse = new SummaryResponse
        {
            Courses = allCoursesOperation.Value.Select(c => new CourseSummary
            {
                Id = c.Id,
                Title = c.Title,
            }).ToList(),

            LastCourse = new CourseExtendedSummary
            {
                Id = lastCourse.Id,
                Description = lastCourse!.Description,
                Title = lastCourse.Title,
                UserApprovedViews =
                    userOperation.Value?.ApprovedViewArticleIds.Count(a => a.CourseId == lastCourse.Id) ?? 0,
                ArticlesCount = lastCourse.Articles.Count
            }
        };
        var courseFilesOperation = await AddCourseFilesAsync(summaryResponse.Courses);
        if (!courseFilesOperation.Success)
            return new(courseFilesOperation);

        return new(summaryResponse);
    }

    private async Task<OperationResult> AddCourseFilesAsync(List<CourseSummary> courses)
    {
        var getFilesTasks = courses.Select(c => _fileSystemHelper.GetCourseFilesAsync(c.Id)).ToArray();
        await Task.WhenAll(getFilesTasks);
        for (int i = 0; i < getFilesTasks.Length; i++)
        {
            if (!getFilesTasks[i].Result.Success && getFilesTasks[i].Result.Errors.Count != 0)
                return getFilesTasks[i].Result;

            courses[i].Files = getFilesTasks[i].Result.Value;
        }

        return OperationResult.Ok;
    }
}