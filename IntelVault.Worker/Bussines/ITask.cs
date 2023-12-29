using IntelVault.Worker.model;

namespace IntelVault.Worker.Bussines;

public interface ITask
{
    
    public Task Execute(OpenSourceRequest openSourceRequest);
    public void Start();
    public void Stop();


}