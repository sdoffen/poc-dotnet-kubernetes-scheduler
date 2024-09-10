namespace Scheduler.Infrastructure.Default;

public class KubernetesOptions
{
    public static readonly string SectionName = "Kubernetes";

    public string Namespace { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public int ServicePort { get; set; }
}
