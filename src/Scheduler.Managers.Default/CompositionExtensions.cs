using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduler.Managers.Default;

public static class CompositionExtensions
{
    public static IServiceCollection RegisterManagers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IScheduleManager, ScheduleManager>();
        return services;
    }
}
