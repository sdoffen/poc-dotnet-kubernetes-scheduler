using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Scheduler.Infrastructure.Default.Tests;

public class IntegrationTestFixture
{
    private readonly IServiceProvider _provider;

    public IntegrationTestFixture()
    {
        var configuration = new ConfigurationManager();
        configuration["ASPNETCORE_ENVIRONMENT"] = "Development";

        var services = new ServiceCollection();
        services.AddLogging(builder =>
        {
            builder.AddDebug();
            builder.AddConsole();
        });

        services.RegisterInfrastructure(configuration);
        _provider = services.BuildServiceProvider();
    }

    /// <summary>
    /// Get service of type T from the IServiceProvider.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>A service object of type T.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T GetRequiredService<T>() where T : notnull => _provider.GetRequiredService<T>();
}
