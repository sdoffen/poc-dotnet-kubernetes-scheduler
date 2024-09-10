using Cron.Extensions.Expressions;
using Scheduler.DomainModels;
using Scheduler.Infrastructure;

namespace Scheduler.Managers.Default;

public class ScheduleManager : IScheduleManager
{
    private readonly ICronClient _cronClient;

    private static readonly string _prefix = "scheduler";

    public ScheduleManager(ICronClient cronClient)
    {
        _cronClient = cronClient;
    }

    public async Task CreateScheduleAsync(string jobName)
    {
        var schedule = new CronExpression();

        var cronJob = new CronJob
        {
            Name = GetJobName(jobName),
            Schedule = schedule.ToCronExpression(),
            Target = new($"http://{_cronClient.ServiceName}:{_cronClient.ServicePort}/api/jobs/{jobName}")
        };

        await _cronClient.CreateCronJobAsync(cronJob);
    }

    public async Task DeleteScheduleAsync(string jobName)
    {
        await _cronClient.DeleteCronJobAsync(GetJobName(jobName));
    }

    public async Task<IEnumerable<CronJob>> GetSchedulesAsync()
    {
        return await _cronClient.GetCronJobsAsync();
    }

    private static string GetJobName(string jobName)
    {
        return $"{_prefix}-{jobName.ToLowerInvariant()}";
    }
}
