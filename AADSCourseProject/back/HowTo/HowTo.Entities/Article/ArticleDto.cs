using System;
using System.ComponentModel.DataAnnotations;
using HowTo.Entities.Course;

namespace HowTo.Entities.Article;

public class ArticleDto
{
    [Required] public int Id { get; set; }
    
    [Required] public virtual CourseDto Course { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string FullPath { get; set; }

    [Required] public DateTimeOffset CreatedAt { get; set; }
    [Required] public DateTimeOffset UpdatedAt { get; set; }
    [Required] public User Author { get; set; }
}