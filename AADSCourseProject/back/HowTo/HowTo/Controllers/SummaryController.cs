using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HowTo;

public class SummaryController : Controller
{
    private readonly SummaryManager _summaryManager;

    public SummaryController(SummaryManager summaryManager)
    {
        _summaryManager = summaryManager;
    }

    /// <summary>
    /// Получение информации по существующим курсам
    /// </summary>
    [HttpGet]
    [Route("api/summary/courses")]
    public Task<IActionResult> GetSummaryCourses()
    {
        return _summaryManager.GetSummaryAsync().AsActionResultAsync();
    }
}