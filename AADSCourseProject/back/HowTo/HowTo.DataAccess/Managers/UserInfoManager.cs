using System;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.Article;
using HowTo.Entities.UserInfo;

namespace HowTo.DataAccess.Managers;

public class UserInfoManager
{
    private readonly UserInfoRepository _userInfoRepository;

    public UserInfoManager(
        UserInfoRepository userInfoRepository)
    {
        _userInfoRepository = userInfoRepository;
    }

    public Task<OperationResult<UserInfoDto>> AddApprovedViewAsync(User user, int articleId)
    {
        return _userInfoRepository.AddApprovedViewAsync(user, articleId);
    }

    public Task<OperationResult<UserInfoDto>> SetLastReadCourseIdAsync(User user, int articleId)
    {
        return _userInfoRepository.SetLastReadCourseIdAsync(user, articleId);
    }
    
    public Task<OperationResult<UserInfoDto>> GetUserInfoAsync(User user)
    {
        return _userInfoRepository.GetUserInfoAsync(user);
    }
}