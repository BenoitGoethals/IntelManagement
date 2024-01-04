using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using IntelVault.Worker.model;
using Microsoft.AspNetCore.Components;
using Quartz;

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
        return Task.FromResult(new Status(){
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
            SourceType = (OpenSourceType) openSourceRequestScan.OpenSourceType,
            Interval= openSourceRequestScan.Interval,
        };

        foreach (var keyword in openSourceRequestScan.List.Keyword)
        {
            request.KeyWords.Add(keyword.Name);
        }
        return request;
    }


    public override Task<IsRunning> IsWorkerRunning(Empty request, ServerCallContext context)
    {
        return Task.FromResult(new IsRunning(){Running = Scheduler.IsStarted });
    }

    public override async Task AllJobsRunning(Empty request, IServerStreamWriter<Job> responseStream, ServerCallContext context)
    {
        var dsp = Scheduler.GetCurrentlyExecutingJobs(CancellationToken.None).GetAwaiter().GetResult().ToObservable().Subscribe(
            onNext:  item =>  responseStream.WriteAsync(new Job(){Name = item.JobDetail.Description}),
            onError: ex => Console.WriteLine($"Error: {ex.Message}"),
            onCompleted: () => Console.WriteLine("Observable completed")
            );

        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500); // Gotta look busy

           

        //    await responseStream.WriteAsync(new TempatureReply() { Message = forecast });
        }
    }
}