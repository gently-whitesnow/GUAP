using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.ViewedEntity;
using HowTo.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HowTo.DataAccess.Repositories;

public class ViewRepository
{
    private readonly ApplicationContext _db;

    public ViewRepository(ApplicationContext applicationContext)
    {
        _db = applicationContext;
    }

    public async Task<OperationResult<ViewDto>> UpsertViewAsync(int courseId, int articleId, User user)
    {
        try
        {
            var viewDto = await _db.ViewContext
                .Include(d=>d.Viewers)
                .SingleOrDefaultAsync(v => v.CourseId == courseId && v.ArticleId == articleId);
            if (viewDto == null)
            {
                viewDto = new ViewDto
                {
                    CourseId = courseId,
                    ArticleId = articleId,
                    Viewers = new List<UserGuid> { new(user.Id) }
                };
                await _db.ViewContext.AddAsync(viewDto);
            }
            else
            {
                if (viewDto.Viewers.All(userEntity => userEntity.UserId != user.Id))
                    viewDto.Viewers.Add(new(user.Id));
            }

            await _db.SaveChangesAsync();
            return new(viewDto);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    public async Task<OperationResult<List<ViewDto>>> GetViewsAsync(int courseId, int? articleId = null)
    {
        try
        {
            return new(await _db.ViewContext
                .Include(d=>d.Viewers)
                .Where(v => v.CourseId == courseId 
                            && (articleId == null || v.ArticleId == articleId))
                .ToListAsync());
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
}