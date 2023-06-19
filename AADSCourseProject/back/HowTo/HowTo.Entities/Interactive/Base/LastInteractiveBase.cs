using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HowTo.Entities.Interactive.Base;

public class LastInteractiveBase
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ArticleId { get; set; }

    public Guid UserId { get; set; }
}