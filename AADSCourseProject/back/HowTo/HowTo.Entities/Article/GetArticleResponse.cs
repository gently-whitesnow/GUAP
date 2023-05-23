using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace HowTo.Entities.Article;

public record GetArticleResponse(ArticlePublic Article, List<byte[]> Files);