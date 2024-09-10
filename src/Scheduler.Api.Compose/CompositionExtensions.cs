using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Infrastructure.Default;
using Scheduler.Managers.Default;

namespace Scheduler.Api.Compose;

public static class CompositionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterManagers(configuration);
        services.RegisterInfrastructure(configuration);

        return services;
    }
}
