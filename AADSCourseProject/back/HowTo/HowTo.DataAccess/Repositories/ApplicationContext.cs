using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;
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