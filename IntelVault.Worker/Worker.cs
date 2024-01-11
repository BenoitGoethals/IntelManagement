using IntelVault.Worker.Bussines;
using IntelVault.Worker.model;
using IntelVault.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl.Matchers;
using IScheduler = Quartz.IScheduler;

namespace IntelVault.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IScheduler _scheduler;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IScheduler scheduler, PoolRequests poolRequests,IServiceProvider serviceProvider)
        {
            _logger = logger;
            _scheduler = scheduler;
            _obs=poolRequests;
            _serviceProvider=serviceProvider;
            
        }

       

        private readonly PoolRequests _obs;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _obs.Subscribe( x =>
            {
                if (!_scheduler.IsStarted)
                {
                    _scheduler.Start(stoppingToken);
                }
                var m = new JobDataMap();
                m.Put(nameof(OpenSourceRequest), x);
                switch (x.SourceType)
                {
                    case OpenSourceType.Scrapper:
                        var job = JobBuilder.Create<WebSiteScrapperJob>()
                            .WithIdentity(x.Id.ToString(), "groupScrapper") 
                            .WithDescription(x.Name)
                            .UsingJobData(m)
                            .Build();
                        var trigger = TriggerBuilder.Create()
                            .WithIdentity(x.Id.ToString(), "groupScrapper")
                           .StartAt(x.Start)
                            .EndAt(x.End)
                            
                            .WithDescription(x.Name)
                            .WithSimpleSchedule(xy => xy
                                .WithIntervalInHours((int)x.Interval)
                                .RepeatForever())
                            
                            .Build();
                         _scheduler.ScheduleJob(job, trigger, stoppingToken);
                        break;
                    case OpenSourceType.Api:
                  
                        var jobApi = JobBuilder.Create<RestApiScrapperJob>()
                            .WithIdentity(x.Id.ToString(), "groupScrapperApi")
                            .UsingJobData(m)
                            .WithDescription(x.Name)
                            .Build();
                        var triggerApi = TriggerBuilder.Create()
                            .WithIdentity(x.Id.ToString(), "groupScrapperApi")
                            .StartAt(x.Start)
                            .WithDescription(x.Name)
                            .EndAt(x.End)
                            .WithSimpleSchedule(xy => xy
                                .WithIntervalInHours((int)x.Interval)
                                .RepeatForever())

                            .Build();
                        _scheduler.ScheduleJob(jobApi, triggerApi, stoppingToken);
                        break;
                }
            });
          
          
            _logger.LogInformation("WORKER STARTED");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
            await _scheduler.Shutdown(stoppingToken);
        }
    }
}
