using System.Collections.Generic;
using System.Threading.Tasks;
using ATI.Services.Common.Behaviors;
using HowTo.DataAccess.Repositories;
using HowTo.Entities;
using HowTo.Entities.Views;

namespace HowTo.DataAccess.Managers;

public class ViewManager
{
    private readonly ViewRepository _viewRepository;    
    public ViewManager(ViewRepository viewRepository)
    {
        _viewRepository = viewRepository;
    }

    public Task<OperationResult<ViewDto>> AddViewAsync(int courseId, int articleId, User user)
    {
        return _viewRepository.UpsertViewAsync(courseId, articleId, user);
    }
}