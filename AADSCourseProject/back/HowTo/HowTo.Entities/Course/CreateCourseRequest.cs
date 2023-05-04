namespace HowTo.Entities.Course;

public class CreateCourseRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Path { get; set; }
    public MultipartFormDataContent Image { get; set; }
}