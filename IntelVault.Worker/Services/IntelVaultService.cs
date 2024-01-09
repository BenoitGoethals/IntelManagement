using System;
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
        
        // Iterate through each job key and get the job details
        foreach (var jobKey in jobKeys)
        {
            IJobDetail? jobDetail = await scheduler.GetJobDetail(jobKey);
            IReadOnlyCollection<ITrigger> triggers = await scheduler.GetTriggersOfJob(jobKey);
            foreach (var trigger in triggers)
            {
                jobs.Job.Add(new Job()
                {
                    Name = jobDetail?.Key.Name,
                    Description = jobDetail?.Description,
                    StartDate = trigger.StartTimeUtc.ToTimestamp(),
                    EndDate = trigger.StartTimeUtc.ToTimestamp(),
                    Timetriggerd = 1,
                    Next = trigger.GetNextFireTimeUtc()?.ToTimestamp()
                        
                });
            }
        }
        return await Task.FromResult(jobs);

    }

    public override async Task NewsDocumentAdded(Empty request, IServerStreamWriter<OpenSourceRequestScan> responseStream, ServerCallContext context)
    {
        var disp=poolRequests.Subscribe(x =>
        {
            responseStream.WriteAsync(new OpenSourceRequestScan()
            {
                Id = Guid.NewGuid().ToString(),
                Name = x.Name,
                Url = x.Url,
                Start = x.Start.ToTimestamp(),
                End = x.End.ToTimestamp(),
                OpenSourceType = (OpenSourceMediaType) x.SourceType,
                Interval = x.Interval
               
            });
        });

        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500); // Gotta look busy


        }
        
        disp.Dispose();
    }
}