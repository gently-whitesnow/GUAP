using System;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.Course;
using Microsoft.EntityFrameworkCore;

namespace HowTo.DataAccess.Repositories;

public class ArticleRepository
{
    private readonly ApplicationContext _db;

    public ArticleRepository(ApplicationContext applicationContext)
    {
        _db = applicationContext;
    }

    public async Task<OperationResult<ArticleDto>> InsertArticleAsync(UpsertArticleRequest request, User user, CourseDto course)
    {
        var dto = new ArticleDto
        {
            Course = course,
            Title = request.Title,
            FullPath = request.FullPath,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Author = user
        };
        try
        {
            await _db.ArticleDtos.AddAsync(dto);
            await _db.SaveChangesAsync();
            return new(dto);
        }
        catch (Exception ex)
        {
            return new OperationResult<ArticleDto>(ex);
        }
    }

    public async Task<OperationResult<ArticleDto>> UpdateArticleAsync(UpsertArticleRequest request, User user)
    {
        try
        {
            var articleDto = await _db.ArticleDtos.FirstOrDefaultAsync(c => c.Id == request.ArticleId);
            if (articleDto == null)
            {
                return new(ActionStatus.NotFound, "article_not_found", $"Article with id {request.ArticleId} not found");
            }

            articleDto.UpdatedAt = DateTime.UtcNow;
            articleDto.Title = request.Title;
            articleDto.FullPath = request.FullPath;

            await _db.SaveChangesAsync();
            return new(articleDto);
        }
        catch (Exception ex)
        {
            return new OperationResult<ArticleDto>(ex);
        }
    }
    
    public async Task<OperationResult<ArticleDto>> GetArticleByPathAsync(string articlePath)
    {
        try
        {
            var articleDto = await _db.ArticleDtos.FirstOrDefaultAsync(c => c.FullPath == articlePath);
            if (articleDto == null)
            {
                return new(ActionStatus.NotFound, "article_not_found", $"Article with path {articlePath} not found");
            }

            return new(articleDto);
        }
        catch (Exception ex)
        {
            return new OperationResult<ArticleDto>(ex);
        }
    }

    public async Task<OperationResult<ArticleDto>> DeleteArticleByIdAsync(int articleId)
    {
        try
        {
            var articleDto = await _db.ArticleDtos.FirstOrDefaultAsync(c => c.Id == articleId);
            if (articleDto == null)
            {
                return new(ActionStatus.NotFound, "Article_not_found", $"Article with id {articleId} not found");
            }
            _db.ArticleDtos.Remove(articleDto);
            await _db.SaveChangesAsync();
            return new(articleDto);
        }
        catch (Exception ex)
        {
            return new OperationResult<ArticleDto>(ex);
        }
    }
}