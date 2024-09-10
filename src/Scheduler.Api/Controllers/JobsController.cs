using Microsoft.AspNetCore.Mvc;

namespace Scheduler.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly ILogger<JobsController> _logger;

    public JobsController(ILogger<JobsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{jobName}")]
    public ActionResult<int> Get(string jobName)
    {
        var header = Request.Headers.FirstOrDefault(h => h.Key.Equals("X-Custom-Header", StringComparison.CurrentCultureIgnoreCase));
        if (header.Value.Count == 0)
        {
            _logger.LogInformation("Header X-Custom-Header not found");
        }
        else
        {
            _logger.LogInformation($"Header X-Custom-Header found with value {header.Value}");
        }

        var number = Random.Shared.Next(1, 100);
        var message = $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm tt")} : {jobName} : Getting a random number {number}";
        _logger.LogInformation(message);
        return Ok(message);
    }
}
