using System.Collections.Generic;

namespace HowTo.Entities.Article;

public record GetArticleResponse(ArticlePublic Article, List<byte[]> Files);