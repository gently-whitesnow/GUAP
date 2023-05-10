using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
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

    public async Task<OperationResult<ViewDto>> UpsertViewAsync(int articleId, User user)
    {
        try
        {
            var viewDto = await _db.ViewDtos.FirstOrDefaultAsync(v => v.Id == articleId);
            if (viewDto == null)
            {
                viewDto = new ViewDto
                {
                    Id = articleId,
                    Viewers = new List<UserIdEntity> { new (user.Id) }
                };
                await _db.ViewDtos.AddAsync(viewDto);
            }
            else
            {
                if (viewDto.Viewers.All(userEntity => userEntity.Id != user.Id))
                    viewDto.Viewers.Add(new (user.Id));
            }

            await _db.SaveChangesAsync();
            return new(viewDto);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }
    
    public async Task<OperationResult<ViewDto>> GetViewAsync(int entityId)
    {
        try
        {
            var viewDto = await _db.ViewDtos.FirstOrDefaultAsync(u => u.Id == entityId);
            if (viewDto == null)
            {
                return new(ActionStatus.NotFound, "view_not_found", $"view with id {entityId} not found");
            }

            return new(viewDto);
        }
        catch (Exception ex)
        {
            return new (ex);
        }
    }
}