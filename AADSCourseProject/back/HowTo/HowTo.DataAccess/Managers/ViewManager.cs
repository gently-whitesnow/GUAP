using ATI.Services.Common.Behaviors;
using Microsoft.AspNetCore.Mvc;

namespace HowTo.DataAccess.Managers;

public class ViewManager
{
    private readonly ViewManager _viewManager;


    public ViewManager(ViewManager viewManager)
    {
        _viewManager = viewManager;
    }
    
    public async Task<OperationResult> AddApprovedViewAsync(Guid articleId)
    {
        return new (ActionStatus.Ok);
    }
}