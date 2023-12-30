using Quartz;

namespace IntelVault.Worker;

public class CustomQuartzHostedService
{
    private readonly IScheduler _scheduler;
    public CustomQuartzHostedService(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _scheduler?.Start(cancellationToken);
    }
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _scheduler?.Shutdown(cancellationToken);
    }
}