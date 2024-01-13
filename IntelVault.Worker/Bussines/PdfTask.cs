using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using Quartz;

namespace IntelVault.Worker.Bussines;


public class PdfTask : RequestJob, IJob
{

    private readonly ILogger? _logger;
    private PoolRequests? _poolRequests;
    private IIntelService<IntelDocument>? _intelService;

    public PdfTask(ILogger? logger, PoolRequests? poolRequests, IIntelService<IntelDocument>? intelService)
    {
        _logger = logger;
        _poolRequests = poolRequests;
        _intelService = intelService;
    }

    public override Task Execute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}