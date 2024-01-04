using Quartz;
using Quartz.Spi;

namespace IntelVault.Worker;

public class JobFactory(IServiceProvider serviceProvider) : IJobFactory
{
    public IJob? NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
    }

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        disposable?.Dispose();
    }
}