using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using IntelVault.Worker.Bussines;
using IntelVault.Worker.model;
using Quartz;

namespace IntelVault.Worker
{
    public class Worker(ILogger<Worker> logger, IScheduler sched) : BackgroundService
    {
 
        public PoolRequests Obs { get; private set; } = new();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            Obs.Subscribe(async x =>
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
                            .WithIdentity("myJob", "group1")
                            
                            .WithSimpleSchedule(x => x
                                .WithIntervalInSeconds(5)
                                .RepeatForever())
                            .Build();
                        await sched.ScheduleJob(job, trigger, stoppingToken);
                        await sched.Start(stoppingToken);
                        break;
                }

            });
            Obs.AddRequest(new OpenSourceRequest() { Url = "wwww.googel.be", SourceType = OpenSourceType.Scrapper, KeyWords = new List<string>() });
            await sched.Start(stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
              
                await Task.Delay(1000, stoppingToken);
               
            }
        }
    }
}
