using System.Diagnostics.Metrics;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using IntelVault.Worker.model;
using Microsoft.AspNetCore.Components;
using Quartz;
using Quartz.Impl.Matchers;
using static Quartz.Logging.OperationName;

namespace IntelVault.Worker.Services;

public class IntelVaultService(ILogger<IntelVaultService> logger, PoolRequests poolRequests, IScheduler scheduler)
    : Greeter.GreeterBase
{
    public PoolRequests PoolRequests { get; } = poolRequests;
    public IScheduler Scheduler { get; } = scheduler;


    public override Task<HelloReply> SayHello(HelloRequest request,
        ServerCallContext context)
    {
        return Task.FromResult(new HelloReply()
        {
            Message = "Hello "
        });
    }

    public override Task<Status> MakeJob(OpenSourceRequestScan request, ServerCallContext context)
    {
        PoolRequests.AddRequest(OpenSourceRequestMapOpenSourceRequest(request));
        logger.LogInformation("Saying hello to {Name}", request.Id);
        return Task.FromResult(new Status()
        {
            Message = "Hello " + request.Url
        });
    }


    private OpenSourceRequest OpenSourceRequestMapOpenSourceRequest(OpenSourceRequestScan openSourceRequestScan)
    {
        OpenSourceRequest request = new OpenSourceRequest()
        {
            Name = openSourceRequestScan.Name,
            Url = openSourceRequestScan.Url,
            Id = new Guid(openSourceRequestScan.Id),
            End = openSourceRequestScan.End.ToDateTime(),
            Start = openSourceRequestScan.Start.ToDateTime(),
            KeyWords = new List<string>(),
            SourceType = (OpenSourceType)openSourceRequestScan.OpenSourceType,
            Interval = openSourceRequestScan.Interval,
        };

        foreach (var keyword in openSourceRequestScan.List.Keyword)
        {
            request.KeyWords.Add(keyword.Name);
        }
        return request;
    }


    public override Task<IsRunning> IsWorkerRunning(Empty request, ServerCallContext context)
    {
        return Task.FromResult(new IsRunning() { Running = Scheduler.IsStarted });
    }

    public override async Task<ListJobsRunning> AllJobsRunning(Empty request, ServerCallContext context)
    {
        var jobs = new ListJobsRunning();
        // Get all job keys
        var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

        // List to store scheduled jobs
        List<IJobDetail?> scheduledJobs = new List<IJobDetail?>();

        // Iterate through each job key and get the job details
        foreach (var jobKey in jobKeys)
        {
            IJobDetail? jobDetail = await scheduler.GetJobDetail(jobKey);
            scheduledJobs.Add(jobDetail);
        }

        // Now, scheduledJobs list contains all scheduled jobs

        // Display information about the scheduled jobs
        foreach (var job in scheduledJobs)
        {
            jobs.Job.Add(new Job() { Name = job?.Key.Name });
        }

      
        return await Task.FromResult(jobs);
       
    }

    public override async Task NewsDocumentAdded(Empty request, IServerStreamWriter<NewsItem> responseStream, ServerCallContext context)
    {
        poolRequests.Subscribe(x => { responseStream.WriteAsync(new NewsItem(){Title = x.Name,Content = x.Url,PublishedDate = x.Start.ToTimestamp()});});

        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500); // Gotta look busy

          
        }

    }
}