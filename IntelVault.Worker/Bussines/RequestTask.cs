using IntelVault.Worker.model;

namespace IntelVault.Worker.Bussines;

public abstract class RequestTask:ITask
{
    public bool IsRunning { get; private set; }
    public string? Name { get; set; }
    private CancellationToken CancellationToken { get; set; }
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    public abstract Task Execute(OpenSourceRequest openSourceRequest);
    public abstract void Start();
    public abstract void Stop();
}