namespace IntelVault.Infrastructure.Workers;

public interface IWorkersGrpc
{
    Task<IList<QJobs>?> GetJobs();
    Task<bool> IsWorkerRunning();
    Task<string?> MakeJob(OpenSourceRequest request);
}