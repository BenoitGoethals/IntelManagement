using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using IntelVault.Worker;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace IntelVault.Infrastructure.Workers;

public class WorkersGrpc : IWorkersGrpc
{
    private readonly ILogger<WorkersGrpc> _logger;
    private readonly Greeter.GreeterClient _client;

    public WorkersGrpc(ILogger<WorkersGrpc> logger)
    {
        _logger = logger;
        var grpcChannel = GrpcChannel.ForAddress("https://localhost:5001"); // Replace with your gRPC server address
        _client = new Greeter.GreeterClient(grpcChannel);
    }

    public async Task<IList<QJobs>?> GetJobs()
    {
        IList<QJobs> jobs = [];
        try
        {
            var allJobs = (await _client.AllJobsRunningAsync(new Empty()).ResponseAsync.ConfigureAwait(false)).Job;
            foreach (var job in allJobs)
            {
                jobs.Add(new QJobs()
                {
                    Name = job.Name,
                    Description = job.Description,
                    EndDate = job.EndDate.ToDateTime(),
                    StartDate = job.StartDate.ToDateTime(),
                    Next = job.Next.ToDateTime()
                    
                });
            }
            return jobs;
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            _logger.LogError(ex.Message);
        }

        return null;
    }

    public async Task<bool> IsWorkerRunning()
    {
        try
        {
            var ret = await _client.IsWorkerRunningAsync(new Empty()).ResponseAsync;
            return ret.Running;
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            _logger.LogError(ex.Message);
        }

        return false;
    }

    public async Task<string?> MakeJob(OpenSourceRequest request)
    {
        keywordList li = new keywordList();
        var req = new OpenSourceRequestScan
        {
            Start = request.Start.ToUniversalTime().ToTimestamp(),
            End = request.End.ToUniversalTime().ToTimestamp(),
            Url = request.Url,
            Id = Guid.NewGuid().ToString(),
            List = li,
            OpenSourceType = (OpenSourceMediaType)request.SourceType,
            Name = "web " + Guid.NewGuid()
        };
        try
        {
            var response = _client.MakeJobAsync(req);
            var ret = await response.ResponseAsync;
            return ret.Message;
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            _logger.LogError(ex.Message);
        }

        return null;
    }

}