using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace HowTo.Entities.Course;

public class UpsertCourseRequest
{
    public int? CourseId { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [Required] public string Path { get; set; }


    public MultipartFormDataContent? Image { get; set; }
}