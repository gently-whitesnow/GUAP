using System;
using System.ComponentModel.DataAnnotations;
using HowTo.Entities.Contributor;

namespace HowTo.Entities.Article;

public class ArticleDto
{
    [Required] [Key] public int Id { get; set; }
    
    [Required] public int CourseId { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required] public DateTimeOffset CreatedAt { get; set; }
    [Required] public DateTimeOffset UpdatedAt { get; set; }
    [Required] public virtual ContributorEntity Author { get; set; }
}