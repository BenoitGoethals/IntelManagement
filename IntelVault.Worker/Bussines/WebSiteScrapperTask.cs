using IntelVault.Worker.model;
using System.Diagnostics.Metrics;
using System.Threading;
using Quartz;

namespace IntelVault.Worker.Bussines;
[DisallowConcurrentExecution]
public class WebSiteScrapperTask() :RequestTask, IJob
{
    private OpenSourceRequest _openSourceRequest;

    public override async Task Execute(IJobExecutionContext context)
    {
        var jobDetailJobData = context.JobDetail.JobDataMap[nameof(OpenSourceRequest)] as OpenSourceRequest;
        _openSourceRequest = jobDetailJobData;
        Console.WriteLine("------------>");
        await Task.Delay(1000);

    }

}