using HowTo.DataAccess.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HowTo;

public class CourseController:Controller
{
    private readonly CourseManager _courseManager;

    public CourseController( CourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    /// <summary>
    /// Получение всех курсов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("courses")]
    public IActionResult GetAuth()
    {
        return Ok();
        // return _courseManager.GetCourses();
    }
}