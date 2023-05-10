using System.Net.Http;

namespace HowTo.Entities.Article;

public record GetArticleResponse(ArticleDto Article, MultipartFormDataContent Files);