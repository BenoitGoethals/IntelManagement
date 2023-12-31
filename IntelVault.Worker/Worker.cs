using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using IntelVault.Worker.Bussines;
using IntelVault.Worker.model;
using Quartz;
using IScheduler = Quartz.IScheduler;

namespace IntelVault.Worker
{
    public class Worker(ILogger<Worker> logger, IScheduler schedulder) : BackgroundService
    {
 
        public PoolRequests Obs { get; private set; } = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            Obs.Subscribe( x =>
            {
                switch (x.SourceType)
                {
                    case OpenSourceType.Scrapper:
                       
                        var m = new JobDataMap();
                        m.Put(nameof(x),x);

                        var job = JobBuilder.Create<WebSiteScrapperTask>()
                            .WithIdentity("myJob", "group1") 
                            .UsingJobData(m)
                            .Build();
                        var trigger = TriggerBuilder.Create()
                            .WithIdentity("mytrigger", "group1")
                            .StartNow()
                            .WithSimpleSchedule(x => x
                                .WithIntervalInSeconds(5)
                                .RepeatForever())
                            .Build();
                         schedulder.ScheduleJob(job, trigger, stoppingToken);
                         Task.Delay(1000, stoppingToken);
                        schedulder.Start(stoppingToken);
                        break;
                }

            });
            Obs.AddRequest(new OpenSourceRequest() { Url = "wwww.google.be", SourceType = OpenSourceType.Scrapper, KeyWords = new List<string>() });
            await schedulder.Start(stoppingToken);
            logger.LogInformation("WORKER STARTED");
            while (!stoppingToken.IsCancellationRequested)
            {
              
                await Task.Delay(1000, stoppingToken);
                IReadOnlyCollection<IJobExecutionContext> executingJobs = await schedulder.GetCurrentlyExecutingJobs(stoppingToken);

                // Display information about each executing job
                logger.LogInformation($"Jobs : {executingJobs.Count}");
                foreach (var jobContext in executingJobs)
                {
                    logger.LogInformation($"Job Key: {jobContext.JobDetail.Key}");
                    logger.LogInformation($"Fire Time: {jobContext.FireTimeUtc}");
                    logger.LogInformation($"Next Fire Time: {jobContext.NextFireTimeUtc}");
                    logger.LogInformation("---------------");
                }

            }
            await schedulder.Shutdown(stoppingToken);
        }
    }
}
