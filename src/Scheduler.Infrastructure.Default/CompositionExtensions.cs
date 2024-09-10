using k8s;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduler.Infrastructure.Default;

public static class CompositionExtensions
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KubernetesOptions>(options =>
        {
            options.Namespace = "default";
        });

        // TODO: There might be a better way to check the environment here
        var env = configuration["ASPNETCORE_ENVIRONMENT"];
        if (env == "Development")
        {
            // Use the configuration file to create the Kubernetes client
            services.AddScoped(provider => new Kubernetes(KubernetesClientConfiguration.BuildDefaultConfig()));
        }
        else
        {
            // Use the Kubernetes service account to create the Kubernetes client
            services.AddScoped(provider => new Kubernetes(KubernetesClientConfiguration.InClusterConfig()));
        }

        services.AddScoped<ICronClient, CronClient>();

        return services;
    }
}
