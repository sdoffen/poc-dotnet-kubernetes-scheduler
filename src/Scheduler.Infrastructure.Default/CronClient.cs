using k8s;
using k8s.Models;
using Microsoft.Extensions.Options;
using Scheduler.DomainModels;

namespace Scheduler.Infrastructure.Default;

public class CronClient : ICronClient
{
    private readonly Kubernetes _client;
    private readonly KubernetesOptions _options;

    public CronClient(Kubernetes client, IOptions<KubernetesOptions> options)
    {
        _client = client;
        _options = options.Value;
    }

    public string Namespace => _options.Namespace;

    public string ServiceName => _options.ServiceName;

    public int ServicePort => _options.ServicePort;

    public async Task CreateCronJobAsync(CronJob cronJob)
    {
        var body = new V1CronJob
        {
            Metadata = new V1ObjectMeta
            {
                Name = cronJob.Name,
                NamespaceProperty = _options.Namespace
            },
            Spec = new V1CronJobSpec
            {
                Schedule = cronJob.Schedule,
                JobTemplate = new V1JobTemplateSpec
                {
                    Spec = new V1JobSpec
                    {
                        Template = new V1PodTemplateSpec
                        {
                            Spec = new V1PodSpec
                            {
                                RestartPolicy = "OnFailure",
                                Containers =
                                [
                                    new()
                                    {
                                        Name = $"{cronJob.Name}-container",
                                        Image = "curlimages/curl",
                                        Args =
                                        [
                                            "-H", "X-Custom-Header: SomeValue",
                                            cronJob.Target.ToString()
                                        ]
                                    }
                                ]
                            }
                        }
                    }
                }
            }
        };

        await _client.CreateNamespacedCronJobAsync(body, _options.Namespace);
    }

    public async Task DeleteCronJobAsync(string name)
    {
        await _client.DeleteNamespacedCronJobAsync(name, _options.Namespace);
    }

    public async Task<IEnumerable<CronJob>> GetCronJobsAsync()
    {
        var jobs = await _client.ListNamespacedCronJobAsync(_options.Namespace);

        return jobs.Items.Select(job => new CronJob
        {
            Name = job.Metadata.Name,
            Schedule = job.Spec.Schedule,
            Target = new Uri(job.Spec.JobTemplate.Spec.Template.Spec.Containers.First().Args.Last())
        });
    }
}
