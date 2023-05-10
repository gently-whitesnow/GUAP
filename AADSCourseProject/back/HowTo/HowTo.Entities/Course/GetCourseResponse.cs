using System.Net.Http;

namespace HowTo.Entities.Course;

public record GetCourseResponse(CourseDto Course, MultipartFormDataContent Files);
