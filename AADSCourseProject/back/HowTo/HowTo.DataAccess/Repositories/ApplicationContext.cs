using HowTo.Entities.Article;
using HowTo.Entities.Contributor;
using HowTo.Entities.Course;
using HowTo.Entities.Options;
using HowTo.Entities.UserInfo;
using HowTo.Entities.ViewedEntity;
using HowTo.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HowTo.DataAccess.Repositories;

public class ApplicationContext : DbContext
{
    private DbSettings _options { get; set; }

    public DbSet<ArticleDto> ArticleDtos { get; set; }
    public DbSet<CourseDto> CourseDtos { get; set; }
    public DbSet<ViewDto> ViewDtos { get; set; }
    public DbSet<UserUniqueInfoDto> UserUniqueInfoDtos { get; set; }
    public DbSet<ContributorEntity> ContributorEntityDtos { get; set; }
    public DbSet<ViewedEntity> UserViewEntityDtos { get; set; }
    
    public ApplicationContext(IOptions<DbSettings> options)
    {
        _options = options.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO 
        optionsBuilder.UseSqlite(_options.ConnectionString);
    }
}