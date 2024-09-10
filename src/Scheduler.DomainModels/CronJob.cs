namespace Scheduler.DomainModels;

public class CronJob
{
    public string Name { get; set; } = null!;

    public string Schedule { get; set; } = null!;

    public Uri Target { get; set; } = null!;
}
