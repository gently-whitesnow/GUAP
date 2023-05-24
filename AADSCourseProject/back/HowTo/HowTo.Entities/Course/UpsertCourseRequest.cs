using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HowTo.Entities.Course;

public class UpsertCourseRequest
{
    public int? CourseId { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    public IFormFile? Image { get; set; }
}