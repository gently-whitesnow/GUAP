using HowTo.Entities.Article;
using HowTo.Entities.Contributor;
using HowTo.Entities.Course;
using HowTo.Entities.Options;
using HowTo.Entities.UserInfo;
using HowTo.Entities.ViewedEntity;
using HowTo.Entities.Views;
using Microsoft.Data.Sqlite;
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

    // TODO подумать как можно многопоточно взаимодействовать с sqlite 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connection = new SqliteConnection(_options.ConnectionString);
            connection.Open();

            // Установка уровня изоляции на SERIALIZABLE
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA read_uncommitted = false";
                command.ExecuteNonQuery();
            }
            optionsBuilder.UseSqlite(connection);
        }
    }
}