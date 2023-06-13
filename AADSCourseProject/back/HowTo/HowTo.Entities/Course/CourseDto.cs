using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HowTo.Entities.Article;
using HowTo.Entities.Contributor;

namespace HowTo.Entities.Course;

public class CourseDto
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Title { get; set; }
    [Required]
    [MinLength(10)]
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public DateTimeOffset CreatedAt { get; set; }
    [Required]
    public DateTimeOffset UpdatedAt { get; set; }
    public virtual List<ArticleDto> Articles { get; set; }
}