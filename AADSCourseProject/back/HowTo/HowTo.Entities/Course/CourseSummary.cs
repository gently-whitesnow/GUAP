using System.Collections.Generic;

namespace HowTo.Entities.Course;

public class CourseSummary
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IEnumerable<byte[]> Files { get; set; }
}