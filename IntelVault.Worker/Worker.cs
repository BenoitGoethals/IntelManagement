using IntelVault.Worker.Bussines;
using IntelVault.Worker.model;
using IntelVault.Worker.Services;
using Quartz;
using Quartz.Impl.Matchers;
using IScheduler = Quartz.IScheduler;

namespace IntelVault.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IScheduler _schedulder;

        public Worker(ILogger<Worker> logger, IScheduler schedulder, PoolRequests poolRequests)
        {
            _logger = logger;
            _schedulder = schedulder;
            _obs=poolRequests;
            
        }

       

        private readonly PoolRequests _obs;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _obs.Subscribe( x =>
            {
                switch (x.SourceType)
                {
                    case OpenSourceType.Scrapper:
                        _schedulder.Start(stoppingToken);
                        var m = new JobDataMap();
                        m.Put(nameof(OpenSourceRequest),x);

                        var job = JobBuilder.Create<WebSiteScrapperJob>()
                            .WithIdentity(x.Id.ToString(), "groupScrapper") 
                            .UsingJobData(m)
                            .Build();
                        var trigger = TriggerBuilder.Create()
                            .WithIdentity(x.Id.ToString(), "groupScrapper")
                           .StartAt(x.Start)
                            .EndAt(x.End)
                            .WithSimpleSchedule(xy => xy
                                .WithIntervalInSeconds(x.Interval)
                                .RepeatForever())
                            
                            .Build();
                         _schedulder.ScheduleJob(job, trigger, stoppingToken);
                        break;
                }

            });
          
          
            _logger.LogInformation("WORKER STARTED");
            while (!stoppingToken.IsCancellationRequested)
            {
              
                await Task.Delay(1000, stoppingToken);
                
                

            }
            await _schedulder.Shutdown(stoppingToken);
        }
    }
}
