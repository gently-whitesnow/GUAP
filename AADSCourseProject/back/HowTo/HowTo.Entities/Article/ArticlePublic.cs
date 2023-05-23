using System;
using System.Linq;
using HowTo.Entities.Contributor;
using HowTo.Entities.UserInfo;

namespace HowTo.Entities.Article;

public class ArticlePublic
{
    public ArticlePublic(ArticleDto articleDto,User user, UserUniqueInfoDto userUniqueInfo)
    {
        Id = articleDto.Id;
        CourseId = articleDto.CourseId;
        Title = articleDto.Title;
        CreatedAt = articleDto.CreatedAt;
        UpdatedAt = articleDto.UpdatedAt;
        Author = articleDto.Author;
        IsAuthor = articleDto.Author.UserId == user.Id;
        IsViewed = userUniqueInfo?.ApprovedViewArticleIds.Any(a=>a.ArticleId == Id && a.CourseId == CourseId)
                   ?? false;
    }
    public int Id { get; set; }

    public int CourseId { get; set; }


    public string Title { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public ContributorEntity Author { get; set; }

    public bool IsAuthor { get; set; }
    public bool IsViewed { get; set; }
}