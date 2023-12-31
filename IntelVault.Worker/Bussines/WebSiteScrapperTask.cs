using IntelVault.Worker.model;
using System.Diagnostics.Metrics;
using System.Threading;
using Quartz;

namespace IntelVault.Worker.Bussines;
[DisallowConcurrentExecution]
public class WebSiteScrapperTask(ILogger<WebSiteScrapperTask> logger) :RequestTask
{
    private OpenSourceRequest _openSourceRequest;

    public override Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine(_openSourceRequest);
        if (IsRunning) return Task.CompletedTask;
        IsRunning = true;
        CancellationToken = CancellationTokenSource.Token;
        logger.LogInformation("started");
        while (!CancellationToken.IsCancellationRequested)
        {
            if (!IsRunning) continue;
            _ = Task.Delay(5000, CancellationToken);
            logger.LogInformation("running");
            Console.WriteLine(_openSourceRequest);
        }

        IsRunning = false;
        return Task.CompletedTask;
    }

}