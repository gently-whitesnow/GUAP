using System;
using System.ComponentModel.DataAnnotations;

namespace HowTo.Entities.Interactive.Base;

public class LastInteractiveBase : IHaveId
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ArticleId { get; set; }

    public Guid UserId { get; set; }
}