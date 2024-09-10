using Scheduler.DomainModels;
using Shouldly;

namespace Scheduler.Infrastructure.Default.Tests;

[Collection("IntegrationTest")]
public class CronClientTests
{
    private readonly ICronClient _cronClient;

    public CronClientTests(IntegrationTestFixture fixture)
    {
        _cronClient = fixture.GetRequiredService<ICronClient>();
    }

    [Fact]
    public async Task CreateDeleteListTest()
    {
        var jobs = await _cronClient.GetCronJobsAsync();
        var count = jobs.Count();

        var testJobName = "test-job";

        var newJob = new CronJob
        {
            Name = testJobName,
            Schedule = "* * * * *",
            Target = new Uri("https://www.google.com")
        };

        await _cronClient.CreateCronJobAsync(newJob);

        jobs = await _cronClient.GetCronJobsAsync();
        jobs.Count().ShouldBe(count + 1);


        await _cronClient.DeleteCronJobAsync(testJobName);

        jobs = await _cronClient.GetCronJobsAsync();
        jobs.Count().ShouldBe(count);
    }
}
