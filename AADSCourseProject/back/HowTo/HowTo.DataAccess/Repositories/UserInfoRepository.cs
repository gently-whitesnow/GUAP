using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.UserInfo;
using Microsoft.EntityFrameworkCore;

namespace HowTo.DataAccess.Repositories;

public class UserInfoRepository
{
    private readonly ApplicationContext _db;

    public UserInfoRepository(ApplicationContext applicationContext)
    {
        _db = applicationContext;
    }

    public async Task<OperationResult<UserInfoDto>> SetLastReadCourseIdAsync(User user, int courseId)
    {
        try
        {
            var userInfoDto = await _db.UserInfoDtos.FirstOrDefaultAsync(v => v.Id == user.Id);
            if (userInfoDto == null)
            {
                userInfoDto = new UserInfoDto
                {
                    Id = user.Id,
                    LastReadCourseId = courseId
                };
                await _db.UserInfoDtos.AddAsync(userInfoDto);
            }
            else
            {
                userInfoDto.LastReadCourseId = courseId;
            }

            await _db.SaveChangesAsync();
            return new(userInfoDto);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    public async Task<OperationResult<UserInfoDto>> AddApprovedViewAsync(User user, int articleId)
    {
        try
        {
            var userInfoDto = await _db.UserInfoDtos.FirstOrDefaultAsync(v => v.Id == user.Id);
            if (userInfoDto == null)
            {
                userInfoDto = new UserInfoDto
                {
                    Id = user.Id,
                    ApprovedViewArticleIds = new List<ApprovedViewArticleEntity> { new(articleId)}
                };
                await _db.UserInfoDtos.AddAsync(userInfoDto);
            }
            else
            {
                if (userInfoDto.ApprovedViewArticleIds.All(entity => entity.Id != articleId))
                    userInfoDto.ApprovedViewArticleIds.Add(new(articleId));
            }

            await _db.SaveChangesAsync();
            return new(userInfoDto);
        }
        catch (Exception ex)
        {
            return new(ex);
        }
    }

    public async Task<OperationResult<UserInfoDto>> GetUserInfoAsync(User user)
    {
        try
        {
            var userInfoDto = await _db.UserInfoDtos.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (userInfoDto == null)
            {
                return new(ActionStatus.NotFound, "user_not_found", $"user with id {user.Id} not found");
            }

            return new(userInfoDto);
        }
        catch (Exception ex)
        {
            return new (ex);
        }
    }
}