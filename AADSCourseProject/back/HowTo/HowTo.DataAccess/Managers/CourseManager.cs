using ATI.Services.Common.Behaviors;
using HowTo.Entities.Course;

namespace HowTo.DataAccess.Managers;

public class CourseManager
{
    public async Task<OperationResult> CreateCourseAsync(CreateCourseRequest request)
    {
        return new (ActionStatus.Ok);
    }
    
    
    public async Task<OperationResult> GetCourseAsync(string coursePath)
    {
        return new (ActionStatus.Ok);
    }
    
    public async Task<OperationResult> DeleteCourseAsync(Guid courseId)
    {
        return new (ActionStatus.Ok);
    }
}