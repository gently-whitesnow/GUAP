using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HowTo.Entities.Views;

[PrimaryKey(nameof(CourseId), nameof(ArticleId))]
public class ViewDto
{
    [Required]
    public int CourseId { get; set; }
    
    [Required]
    public int ArticleId { get; set; }
    
    [Required]
    public virtual List<UserGuid> Viewers { get; set; }
}