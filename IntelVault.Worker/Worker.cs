using IntelVault.Worker.Bussines;
using IntelVault.Worker.model;
using Quartz;
using Quartz.Impl.Matchers;
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
                        schedulder.Start(stoppingToken);
                        var m = new JobDataMap();
                        m.Put(nameof(OpenSourceRequest),x);

                        var job = JobBuilder.Create<WebSiteScrapperJob>()
                            .WithIdentity("myJob", "group1") 
                            .UsingJobData(m)
                            .Build();
                        var trigger = TriggerBuilder.Create()
                            .WithIdentity("mytrigger", "group1")
                            .StartNow()
                            .WithSimpleSchedule(x => x
                                .WithIntervalInSeconds(10)
                                .RepeatForever())
                            .Build();
                         schedulder.ScheduleJob(job, trigger, stoppingToken);
                        break;
                }

            });
            await schedulder.Start(stoppingToken);
            Obs.AddRequest(new OpenSourceRequest() { Url = "wwww.google.be", SourceType = OpenSourceType.Scrapper, KeyWords =
                []
            });
          
            logger.LogInformation("WORKER STARTED");
            while (!stoppingToken.IsCancellationRequested)
            {
              
                await Task.Delay(1000, stoppingToken);
                
                

            }
            await schedulder.Shutdown(stoppingToken);
        }
    }
}
