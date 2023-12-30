using IntelVault.Worker.model;
using System.Diagnostics.Metrics;
using Quartz;

namespace IntelVault.Worker.Bussines;

public abstract class RequestTask:IJob
{
    protected bool IsRunning { get;  set; }
    public string? Name { get; set; }
    protected CancellationToken CancellationToken { get; set; }
    protected readonly CancellationTokenSource CancellationTokenSource = new();
   

   
    public abstract Task Execute(IJobExecutionContext context);
   
}