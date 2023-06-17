using HowTo.Entities.Article;
using HowTo.Entities.Contributor;
using HowTo.Entities.Course;
using HowTo.Entities.Interactive.Base;
using HowTo.Entities.Interactive.CheckList;
using HowTo.Entities.Interactive.ChoiceOfAnswers;
using HowTo.Entities.Interactive.ProgramWriting;
using HowTo.Entities.Interactive.WritingOfAnswer;
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

    public DbSet<ArticleDto> ArticleContext { get; set; }
    public DbSet<CourseDto> CourseContext { get; set; }
    public DbSet<ViewDto> ViewContext { get; set; }
    public DbSet<UserUniqueInfoDto> UserUniqueInfoContext { get; set; }
    public DbSet<ContributorEntity> ContributorEntityContext { get; set; }
    public DbSet<ViewedEntity> UserViewEntityContext { get; set; }

    #region Interactive

    public DbSet<InteractiveBase> InteractiveContext { get; set; }
    
    public DbSet<CheckListDto> CheckListContext { get; set; }
    
    public DbSet<ChoiceOfAnswerDto> ChoiceOfAnswerContext { get; set; }
    public DbSet<LogChoiceOfAnswerDto> LogChoiceOfAnswerContext { get; set; }
    
    public DbSet<ProgramWritingDto> ProgramWritingContext { get; set; }
    public DbSet<LogProgramWritingDto> LogProgramWritingContext { get; set; }
    
    public DbSet<WritingOfAnswerDto> WritingOfAnswerContext { get; set; }
    public DbSet<LogWritingOfAnswerDto> LogWritingOfAnswerContext { get; set; }
    
    public DbSet<LastCheckListDto> LastCheckListContext { get; set; }
    public DbSet<LastChoiceOfAnswerDto> LastChoiceOfAnswerContext { get; set; }
    public DbSet<LastProgramWritingDto> LastProgramWritingContext { get; set; }
    public DbSet<LastWritingOfAnswerDto> LastWritingOfAnswerContext { get; set; }
    
    #endregion
    
    
    public ApplicationContext(IOptions<DbSettings> options)
    {
        _options = options.Value;
    }

    // TODO подумать как еще можно многопоточно взаимодействовать с sqlite 
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