using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Model;

public abstract class BaseIntel:MongoEntity
{
    public TypeIntel IntelType { get; set; }

    public string?  ShortContent{ get; set; }

}