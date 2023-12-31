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
    // handy when part of cluster or you want to otherwise identify multiple schedulers
    q.SchedulerId = "Scheduler-Core";

    // we take this from appsettings.json, just show it's possible
    q.SchedulerName = "Quartz ASP.NET Core Scheduler";

    // as of 3.3.2 this also injects scoped services (like EF DbContext) without problems
    q.UseMicrosoftDependencyInjectionJobFactory();
    // or for scoped service support like EF Core DbContext
    //q.UseMicrosoftDependencyInjectionScopedJobFactory();

    // these are the defaults
    q.UseSimpleTypeLoader();
    q.UseInMemoryStore();
    q.UseDefaultThreadPool(tp =>
    {
        tp.MaxConcurrency = 100;
    });

    //// also add XML configuration and poll it for changes
    //q.UseXmlSchedulingConfiguration(x =>
    //{
    //    x.Files = new[] { "~/quartz.config" };
    //    x.ScanInterval = TimeSpan.FromSeconds(2);
    //    x.FailOnFileNotFound = true;
    //    x.FailOnSchedulingError = true;
    //});

    // convert time zones using converter that can handle Windows/Linux differences
    q.UseTimeZoneConverter();

    // auto-interrupt long-running job
    q.UseJobAutoInterrupt(options =>
    {

        // this is the default
        options.DefaultMaxRunTime = TimeSpan.FromMinutes(60);
    });

})

.AddSingleton<Quartz.IScheduler>((sp) => {

    var scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
    return scheduler;
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
//// First we must get a reference to a scheduler
//var properties = new NameValueCollection
//{
//    ["quartz.scheduler.instanceName"] = "QuartzWithCore",
//    ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
//    ["quartz.threadPool.threadCount"] = "3",
//    ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
//};
var host = builder.Build();
host.Run();
