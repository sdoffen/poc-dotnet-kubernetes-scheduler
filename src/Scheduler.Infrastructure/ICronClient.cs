using Scheduler.DomainModels;

namespace Scheduler.Infrastructure;

public interface ICronClient
{
    string Namespace { get; }

    string ServiceName { get; }

    int ServicePort { get; }

    Task CreateCronJobAsync(CronJob cronJob);

    Task<IEnumerable<CronJob>> GetCronJobsAsync();

    Task DeleteCronJobAsync(string name);
}
