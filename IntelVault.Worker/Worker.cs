using IntelVault.Worker.Bussines;
using IntelVault.Worker.model;
using IntelVault.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl.Matchers;
using IScheduler = Quartz.IScheduler;

namespace IntelVault.Worker
{
    public class Worker(
        ILogger<Worker> logger,
        IScheduler scheduler,
        PoolRequests poolRequests,
        IServiceProvider serviceProvider)
        : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            poolRequests.Subscribe( x =>
            {
                if (!scheduler.IsStarted)
                {
                    scheduler.Start(stoppingToken);
                }
                var m = new JobDataMap();
                m.Put(nameof(OpenSourceRequest), x);
                switch (x.SourceType)
                {
                    case OpenSourceType.Twitter:
                        var pas = new JobDataMap();
                        if (x.KeyWords != null)
                        {
                            pas.Put("subjects", x.KeyWords);
                        }
                        var jobtwit = JobBuilder.Create<TwitterTask>()
                            .WithIdentity(x.Id.ToString(), "groupScrapper")
                            .WithDescription(x.Name)
                            .UsingJobData(pas)
                            .Build();
                        var triggertwitt = TriggerBuilder.Create()
                            .WithIdentity(x.Id.ToString(), "groupScrapper")
                            .StartAt(x.Start)
                            .EndAt(x.End)
                            .WithDescription(x.Name)
                            .WithSimpleSchedule(xy => xy
                                //   .WithIntervalInHours((int)x.Interval)
                                .WithIntervalInMinutes(1)
                                .RepeatForever())
                            .Build();
                        scheduler.ScheduleJob(jobtwit, triggertwitt, stoppingToken);
                        break;
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
                             //   .WithIntervalInHours((int)x.Interval)
                             .WithIntervalInMinutes(1)
                                .RepeatForever())
                            .Build();
                         scheduler.ScheduleJob(job, trigger, stoppingToken);
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
                          //        .WithIntervalInHours((int)x.Interval)
                          .WithIntervalInMinutes(1)
                                .RepeatForever())

                            .Build();
                        scheduler.ScheduleJob(jobApi, triggerApi, stoppingToken);
                        break;
                }
            });
          
          
            logger.LogInformation("WORKER STARTED");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
            await scheduler.Shutdown(stoppingToken);
        }
    }
}
