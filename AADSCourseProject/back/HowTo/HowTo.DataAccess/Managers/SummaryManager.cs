using ATI.Services.Common.Behaviors;
using HowTo.Entities.Summary;

namespace HowTo.DataAccess.Managers;

public class SummaryManager
{
    public async Task<OperationResult<SummaryResponse>> GetSummaryAsync()
    {
        return new (ActionStatus.Ok);
    }
}