namespace HowTo.Entities.Course;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Path { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public User Author { get; set; }
    public List<Guid> ArticleIds { get; set; }
}