using System.Threading.Tasks;
using ATI.Services.Common.Behaviors.OperationBuilder.Extensions;
using HowTo.DataAccess.Managers;
using HowTo.Entities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HowTo.Controllers;

[FakeAuthorizationRequired]
public class SummaryController : Controller
{
    private readonly SummaryManager _summaryManager;

    public SummaryController(SummaryManager summaryManager)
    {
        _summaryManager = summaryManager;
    }

    [HttpGet]
    [Route("api/summary/courses")]
    public Task<IActionResult> GetSummaryCoursesAsync()
    {
        var user = HttpContext.GetUser();
        return _summaryManager.GetSummaryAsync(user).AsActionResultAsync();
    }
}