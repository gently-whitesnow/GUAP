using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HowTo.Entities.Article;

public class UpsertArticleRequest
{
    public int? ArticleId { get; set; }
    [Required]
    public int CourseId { get; set; }
    [Required]
    public string Title { get; set; }
    public IFormFile? File { get; set; }
}