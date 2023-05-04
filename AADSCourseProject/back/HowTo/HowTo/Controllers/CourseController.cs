using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using HowTo.Entities.Course;
using Microsoft.AspNetCore.Mvc;

namespace HowTo;

public class CourseController : Controller
{
    private readonly CourseManager _courseManager;

    public CourseController(CourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    /// <summary>
    /// Создание курса
    /// </summary>
    [HttpPost]
    [Route("api/courses")]
    public Task<IActionResult> CreateCourseAsync(CreateCourseRequest request)
    {
        return _courseManager.CreateCourseAsync(request).AsActionResultAsync();
    }

    /// <summary>
    /// Получение информации по курсу
    /// </summary>
    [HttpPost]
    [Route("api/courses/{coursePath}")]
    public Task<IActionResult> GetCourseAsync([FromRoute] string coursePath)
    {
        return _courseManager.GetCourseAsync(coursePath).AsActionResultAsync();
    }
    
    /// <summary>
    /// Удаление курса
    /// </summary>
    [HttpDelete]
    [Route("api/courses/{courseId}")]
    public Task<IActionResult> DeleteArticleAsync([FromRoute] Guid courseId)
    {
        return _courseManager.DeleteCourseAsync(courseId).AsActionResultAsync();
    }
}