using IntelVault.Worker;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddQuartzHostedService(options =>
//{
//    options.StartDelay = TimeSpan.FromSeconds(5);
//    options.WaitForJobsToComplete = true;
//});
builder.Services.AddHostedService<Worker>();


builder.Services.AddQuartz(q =>
{
    q.SchedulerName = "MassTransit-Scheduler";
    q.SchedulerId = "AUTO";

    q.UseMicrosoftDependencyInjectionJobFactory();

    q.UseDefaultThreadPool(tp =>
    {
        tp.MaxConcurrency = 10;
    });


    //q.UsePersistentStore(s =>
    //{
    //    s.UseProperties = true;
    //    s.RetryInterval = TimeSpan.FromSeconds(15);

    // //   s.UseSqlServer(connectionString);

    //    s.UseJsonSerializer();

    //    s.UseClustering(c =>
    //    {
    //        c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
    //        c.CheckinInterval = TimeSpan.FromSeconds(10);
    //    });
    //});
});



builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders(); // Clear other logging providers
    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
    loggingBuilder.AddConsole();
});

builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});
// First we must get a reference to a scheduler
var properties = new NameValueCollection
{
    ["quartz.scheduler.instanceName"] = "QuartzWithCore",
    ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
    ["quartz.threadPool.threadCount"] = "3",
    ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
};
ISchedulerFactory sf = new StdSchedulerFactory(properties);
IScheduler scheduler = sf.GetScheduler().GetAwaiter().GetResult(); ;
builder.Services.AddSingleton<IScheduler>(scheduler);
builder.Services.AddHostedService<QuartzHostedService>();
var host = builder.Build();
host.Run();
