using IntelVault.Worker;
using Microsoft.AspNetCore.Builder;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using FluentValidation;

using IntelVault.Worker.Services;
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.ApplicationCore.validation;
using IntelVault.Infrastructure.repos;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using NLog.Extensions.Logging;
using MongoDB.Bson.Serialization;
using Quartz.Spi;
using System.Reactive.Concurrency;
using IntelVault.Worker.Bussines;
using Quartz.Simpl;
using NLog;
using ILogger = NLog.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddQuartzHostedService(options =>
//{
//    options.StartDelay = TimeSpan.FromSeconds(5);
//    options.WaitForJobsToComplete = true;
//});
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var setting = new MongoClientSettings()
    {
        Scheme = ConnectionStringScheme.MongoDB,
        Server = new MongoServerAddress("localhost", 27017),
        //  Credential = MongoCredential.CreateCredential("IntelVault", "benoit", "ranger14")
    };
    return new MongoClient(setting);
});
builder.Services.AddSingleton<NewsArticleValidator>();

builder.Services.AddSingleton<IMongoDbRepository<NewsArticle>, MongoDbRepository<NewsArticle>>(n => new MongoDbRepository<NewsArticle>(n.GetRequiredService<IMongoClient>(), n.GetRequiredService<ILogger<IMongoDbRepository<NewsArticle>>>(), "IntelVault"));

builder.Services.AddSingleton<IIntelService<NewsArticle>, IntelService<NewsArticle>>(n => new IntelService<NewsArticle>(n.GetRequiredService<IMongoDbRepository<NewsArticle>>(), n.GetRequiredService<NewsArticleValidator>()));

builder.Services.AddSingleton<IIntelService<IntelDocument>, IntelService<IntelDocument>>(n => new IntelService<IntelDocument>(n.GetRequiredService<IMongoDbRepository<IntelDocument>>(), n.GetRequiredService<IntelDocumentValidator>()));

builder.Services.AddSingleton<ServiceCountry>();


builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
//builder.Services.AddQuartzHostedService(options =>
//{
//    // when shutting down we want jobs to complete gracefully
//    options.WaitForJobsToComplete = true;
//});
builder.Services.AddGrpc();
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<IJobFactory, JobFactory>();
builder.Services.AddSingleton<PoolRequests>();
builder.Services.AddQuartz(q =>
{
    // handy when part of cluster or you want to otherwise identify multiple schedulers
    q.SchedulerId = "Scheduler-Core";
  
    // we take this from appsettings.json, just show it's possible
    q.SchedulerName = "QuartzIntel";
  
    // as of 3.3.2 this also injects scoped services (like EF DbContext) without problems
 
  
    // these are the defaults
    q.UseSimpleTypeLoader();
    q.UseInMemoryStore();
    q.UseDefaultThreadPool(tp =>
    {
        tp.MaxConcurrency = 10;
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

}).AddQuartzOpenTracing(options =>
{
    // these are the defaults
    //  options.ComponentName = "Quartz";
    options.IncludeExceptionDetails = true;
}).AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
    // when we need to init another IHostedServices first
    options.StartDelay = TimeSpan.FromSeconds(10);
});
//builder.Services.AddScoped<WebSiteScrapperJob>();
builder.Services.AddSingleton<RestApiScrapperJob>();
builder.Services.AddSingleton<TwitterTask>();
builder.Services.AddSingleton<WebSiteScrapperJob>();
builder.Services.AddSingleton<Quartz.IScheduler>((sp) =>
{
    using var scope = sp.CreateScope();
    var schedulerFactory = scope.ServiceProvider.GetService<ISchedulerFactory>();
    var scheduler =schedulerFactory?.GetScheduler().GetAwaiter().GetResult() ?? throw new InvalidOperationException(); 
    scheduler.JobFactory = new JobFactory(sp);
    return scheduler;
});



builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders(); // Clear other logging providers
    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
    loggingBuilder.AddNLog(builder.Configuration);
    loggingBuilder.AddConsole();
});

BsonClassMap.RegisterClassMap<SocialMedia>(cm =>
{
    cm.AutoMap();
    cm.SetDiscriminator("SocialMedia");
});

builder.Services.AddGrpc();

var host = builder.Build();


host.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
host.MapGrpcService<IntelVaultService>().EnableGrpcWeb(); ;
host.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

host.Run();
