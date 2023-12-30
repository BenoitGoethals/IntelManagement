using Quartz;
using Quartz.Spi;

namespace IntelVault.Worker;
public class CustomQuartzJobFactory(IServiceProvider serviceProvider) : IJobFactory
{
    public IJob? NewJob(TriggerFiredBundle triggerFiredBundle,
        IScheduler scheduler)
    {
        var jobDetail = triggerFiredBundle.JobDetail;
        return (IJob)serviceProvider.GetService(jobDetail.JobType);
    }
    public void ReturnJob(IJob job) { }
}