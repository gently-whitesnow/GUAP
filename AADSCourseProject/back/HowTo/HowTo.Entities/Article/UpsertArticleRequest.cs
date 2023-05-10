using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace HowTo.Entities.Article;

public class UpsertArticleRequest
{
    public int? ArticleId { get; set; }
    [Required]
    public int CourseId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string FullPath { get; set; }
    [Required]
    public MultipartFormDataContent Files { get; set; }
}