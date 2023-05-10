using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.Course;
using HowTo.Entities.Summary;

namespace HowTo.DataAccess.Managers;

public class SummaryManager
{
    private readonly UserInfoManager _userInfoManager;
    private readonly CourseManager _courseManager;

    public SummaryManager(UserInfoManager userInfoManager,
        CourseManager courseManager)
    {
        _userInfoManager = userInfoManager;
        _courseManager = courseManager;
    }

    public async Task<OperationResult<SummaryResponse>> GetSummaryAsync(User user)
    {
        var userOperation = await _userInfoManager.GetUserInfoAsync(user);
        if (userOperation is { Success: false, ActionStatus: ActionStatus.InternalServerError })
            return new(userOperation);

        var allCoursesOperation = await _courseManager.GetAllCoursesAsync();
        if (allCoursesOperation is { Success: false, ActionStatus: ActionStatus.InternalServerError })
            return new(allCoursesOperation);

        var lastCourse = allCoursesOperation.Value.FirstOrDefault(c => c.Id == userOperation.Value.LastReadCourseId) ??
                         allCoursesOperation.Value.FirstOrDefault(c => c.Id == 1);
        var lastExtendedInfoCourse = lastCourse == null
            ? null
            : new CourseExtendedSummary
            {
                Description = lastCourse?.Description,
                Title = lastCourse.Title,
                Path = lastCourse.Path
            };
        
        return new(new SummaryResponse
        {
            Courses = allCoursesOperation.Value.Select(c => new CourseSummary
            {
                Path = c.Path,
                Title = c.Title,
            }).ToList(),
            LastCourse = lastExtendedInfoCourse
        });
    }
}