using Quartz.Impl;
using Quartz.Spi;
using Quartz;

namespace IntelVault.Worker;

public class JobSchedule
{
    public JobSchedule(Type jobType, string cronExpression)
    {
        JobType = jobType;
        CronExpression = cronExpression;
    }

    public Type JobType { get; }
    public string CronExpression { get; }
}
