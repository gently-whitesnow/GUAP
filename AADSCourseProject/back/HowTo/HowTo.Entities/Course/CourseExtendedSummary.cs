namespace HowTo.Entities.Course;

public class CourseExtendedSummary : CourseSummary
{
    public string Description { get; set; }
    public int UserApprovedViews { get; set; }
    public int ArticlesCount { get; set; }
}