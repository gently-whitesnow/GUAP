using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using HowTo.Entities.Course;
using HowTo.Entities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HowTo.Controllers;

[FakeAuthorizationRequired]
public class CourseController : Controller
{
    private readonly CourseManager _courseManager;

    public CourseController(CourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    /// <summary>
    /// Добавление/обновление курса
    /// </summary>
    [HttpPost]
    [Route("api/courses")]
    [ValidateModelState]
    public Task<IActionResult> UpsertCourseAsync([FromForm] UpsertCourseRequest request)
    {
        var user = HttpContext.GetUser();
        return _courseManager.UpsertCourseAsync(request, user).AsActionResultAsync();
    }
    
    [HttpGet]
    [Route("api/courses/{courseId}")]
    [ValidateModelState]
    public Task<IActionResult> GetCourseAsync([FromRoute][Required] int courseId)
    {
        var user = HttpContext.GetUser();
        return _courseManager.GetCourseWithFilesByIdAsync(courseId, user).AsActionResultAsync();
    }
    
    [HttpDelete]
    [Route("api/courses/{courseId}")]
    [ValidateModelState]
    public Task<IActionResult> DeleteArticleAsync([FromRoute][Required] int courseId)
    {
        return _courseManager.DeleteCourseAsync(courseId).AsActionResultAsync();
    }
}