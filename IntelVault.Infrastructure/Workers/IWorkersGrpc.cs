﻿using System.Collections.ObjectModel;
using ObservableCollections;

namespace IntelVault.Infrastructure.Workers;

public interface IWorkersGrpc
{
    Task<IList<QJobs>?> GetJobs();
    Task<bool> IsWorkerRunning();
    Task<string?> MakeJob(OpenSourceRequest request);
    public Task<ObservableList<QJobs>> GetStreamingJobs();
    public  Task<bool> Start(string job);
    public  Task<bool> Stop(string job);
    public  Task<bool> Delete(string job);
}