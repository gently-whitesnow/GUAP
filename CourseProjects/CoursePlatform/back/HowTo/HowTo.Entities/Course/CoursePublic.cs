using System;
using System.Collections.Generic;
using System.Linq;
using HowTo.Entities.Article;
using HowTo.Entities.Contributor;
using HowTo.Entities.UserInfo;

namespace HowTo.Entities.Course;

public sealed class CoursePublic
{
    public CoursePublic(CourseDto courseDto, User user, UserUniqueInfoDto? userUniqueInfoDto, List<byte[]> files = null)
    {
        Id = courseDto.Id;
        Title = courseDto.Title;
        Description = courseDto.Description;
        CreatedAt = courseDto.CreatedAt;
        UpdatedAt = courseDto.UpdatedAt;
        Contributors = courseDto.Articles?.Select(a => a.Author).DistinctBy(c=>c.UserId);
        Articles = courseDto.Articles?.Select(a => new ArticlePublic(a, user, userUniqueInfoDto));
        Files = files;
        IsAuthor = Contributors == null || Contributors.Any(a => a.UserId == user.Id);
    }

    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }


    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public IEnumerable<ContributorEntity> Contributors { get; set; }
    public IEnumerable<ArticlePublic> Articles { get; set; }

    public IEnumerable<byte[]> Files { get; set; }

    public bool IsAuthor { get; set; }
}