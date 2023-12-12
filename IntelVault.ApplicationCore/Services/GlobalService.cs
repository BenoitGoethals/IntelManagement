using IntelVault.ApplicationCore.Interfaces;
using IntelVault.Infrastructure.repos;
using MongoDB.Bson;

namespace IntelVault.ApplicationCore.Services;

public class GlobalService
{
    public List<IIntelService<BaseIntel>> BaseIntels = new();
    public void Add<T>(IIntelService<BaseIntel> intelService)
    {
        BaseIntels.Add(intelService);
    }


}