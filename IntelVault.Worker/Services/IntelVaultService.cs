using Grpc.Core;

namespace IntelVault.Worker.Services;

public class IntelVaultService:Greeter.GreeterBase
{
    private readonly ILogger<IntelVaultService> _logger;

    public IntelVaultService(ILogger<IntelVaultService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("Saying hello to {Name}", request.Name);
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}