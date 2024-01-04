using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using IntelVault.Worker.model;
using Microsoft.AspNetCore.Components;

namespace IntelVault.Worker.Services;

public class IntelVaultService(ILogger<IntelVaultService> logger, PoolRequests poolRequests)
    : Greeter.GreeterBase
{
    public PoolRequests PoolRequests { get; } = poolRequests;

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

    public override Task AllJobsRunning(Empty request, IServerStreamWriter<ListJobsRunning> responseStream, ServerCallContext context)
    {
        return base.AllJobsRunning(request, responseStream, context);
    }

    public override Task<IsRunning> IsWorkerRunning(Empty request, ServerCallContext context)
    {
        return base.IsWorkerRunning(request, context);
    }

    public override Task NewsDocumentAdded(Empty request, IServerStreamWriter<NewsItem> responseStream, ServerCallContext context)
    {
        return base.NewsDocumentAdded(request, responseStream, context);
    }
}