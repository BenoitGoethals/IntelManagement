using IntelVault.Worker.model;

namespace IntelVault.Worker.Bussines;

public interface ITask
{
    
   
    public Task Start(OpenSourceRequest openSourceRequest);
    public void Stop();


}