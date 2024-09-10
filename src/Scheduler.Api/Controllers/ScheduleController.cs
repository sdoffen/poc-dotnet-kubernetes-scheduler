using Microsoft.AspNetCore.Mvc;
using Scheduler.DomainModels;
using Scheduler.Managers;

namespace Scheduler.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleManager _scheduleManager;

    public ScheduleController(IScheduleManager scheduleManager)
    {
        _scheduleManager = scheduleManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CronJob>>> GetSchedulesAsync()
    {
        return Ok(await _scheduleManager.GetSchedulesAsync());
    }

    [HttpPost("{jobName}")]
    public async Task<IActionResult> CreateScheduleAsync(string jobName)
    {
        await _scheduleManager.CreateScheduleAsync(jobName);
        return Ok();
    }

    [HttpDelete("{jobName}")]
    public async Task<IActionResult> DeleteScheduleAsync(string jobName)
    {
        await _scheduleManager.DeleteScheduleAsync(jobName);
        return Ok();
    }
}
