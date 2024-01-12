using System.Collections.ObjectModel;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using IntelVault.Worker;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Logging;
using ObservableCollections;


namespace IntelVault.Infrastructure.Workers;

public class WorkersGrpc : IWorkersGrpc
{
    private readonly ILogger<WorkersGrpc> _logger;
    private readonly Greeter.GreeterClient _client;
    private readonly ObservableList<QJobs> _jobsList = new ObservableList<QJobs>();

    public WorkersGrpc(ILogger<WorkersGrpc> logger)
    {
        _logger = logger;
        var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    
        var grpcChannel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient }); // Replace with your gRPC server address
        _client = new Greeter.GreeterClient(grpcChannel);

    }

    public Task<ObservableList<QJobs>> GetStreamingJobs()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;
      //  _jobsList.Clear();

          Task backgroundTask = new Task(Action, token);
         backgroundTask.Start(TaskScheduler.Current);

        return  Task.FromResult(_jobsList);

        async void Action()
        {
            try
            {
                var rs = _client.NewsDocumentAdded(new Empty()).ResponseStream;
                await foreach (var job in rs.ReadAllAsync(cancellationToken: token))
                {
                    //await Task.Yield();

                    var jb = new QJobs()
                    {
                        Id = Guid.Parse(job.Id),
                        Name = job.Name,
                        Description = job.Url,
                        EndDate = job.End.ToDateTime(),
                        StartDate = job.Start.ToDateTime(),
                    };
                    if (!_jobsList.Contains(jb))
                    {
                        _jobsList.Add(jb);
                    }
                   
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                _logger.LogError("Stream cancelled.");
                await cancellationTokenSource.CancelAsync();
            }
            await cancellationTokenSource.CancelAsync();
        }
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
                    Group = job.Group,
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

    public async Task<string?> MakeJob(QJobs request)
    {
        keywordList li = new keywordList();
        foreach (var key in request.Keywords)
        {
            li.Keyword.Add(new keyword(){Name = key});
        }
        var req = new OpenSourceRequestScan
        {
            
            Start = request.StartDate?.ToUniversalTime().ToTimestamp(),
            End = request.EndDate?.ToUniversalTime().ToTimestamp(),
            Url = request.Url,
            Id = Guid.NewGuid().ToString(),
            List = li,
            OpenSourceType = (OpenSourceMediaType)request.OpenSourceType,
            Name =  request.Name,
            Interval = request.Interval
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

    public async Task<bool> Start(string? job, string? group)
    {
        try
        {
            var response = await _client.StartAsync(new jobTask(){Name = job,Group = group});
            return response.Status;
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            _logger.LogError(ex.Message);
        }
        return false;
    }

    public async Task<bool> Stop(string? job, string? group)
    {
        try
        {
            var response = await _client.StopAsync(new jobTask(){ Name = job,Group = group});
            return response.Status;
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            _logger.LogError(ex.Message);
        }
        return false;
    }

    public async Task<bool> Delete(string? job, string? group)
    {
        try
        {
            var response = await _client.DeleteAsync(new jobTask(){Name = job,Group = group});
            return response.Status;
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            _logger.LogError(ex.Message);
        }
        return false;
    }
}
