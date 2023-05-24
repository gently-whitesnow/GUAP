using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.UserInfo;
using HowTo.Entities.ViewedEntity;

namespace HowTo.DataAccess.Managers;

public class UserInfoManager
{
    private readonly UserInfoRepository _userInfoRepository;

    public UserInfoManager(UserInfoRepository userInfoRepository)
    {
        _userInfoRepository = userInfoRepository;
    }

    public Task<OperationResult<UserUniqueInfoDto>> AddApprovedViewAsync(User user, AddApprovedViewRequest request)
    {
        return _userInfoRepository.AddApprovedViewAsync(user, request);
    }

    public Task<OperationResult<UserUniqueInfoDto>> SetLastReadCourseIdAsync(User user, int articleId)
    {
        return _userInfoRepository.SetLastReadCourseIdAsync(user, articleId);
    }
    
    public Task<OperationResult<UserUniqueInfoDto>> GetUserInfoAsync(User user)
    {
        return _userInfoRepository.GetUserInfoAsync(user);
    }
}