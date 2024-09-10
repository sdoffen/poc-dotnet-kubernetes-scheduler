using Scheduler.DomainModels;

namespace Scheduler.Managers;

public interface IScheduleManager
{
    Task CreateScheduleAsync(string jobName);

    Task DeleteScheduleAsync(string jobName);

    Task<IEnumerable<CronJob>> GetSchedulesAsync();
}
