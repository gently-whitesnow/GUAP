using System.Collections.Generic;
using HowTo.Entities.Course;

namespace HowTo.Entities.Summary;

public class SummaryResponse
{
    public CourseExtendedSummary? LastCourse { get; set; }
    public List<CourseSummary> Courses { get; set; }
}