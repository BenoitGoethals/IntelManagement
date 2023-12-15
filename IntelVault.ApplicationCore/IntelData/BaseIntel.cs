using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.Infrastructure.repos;
using MongoDB.Bson;

namespace IntelVault.ApplicationCore.IntelData;

public abstract class BaseIntel:MongoEntity
{
    public TypeIntel IntelType { get; set; }

    public string?  ShortContent{ get; set; }

    public static explicit operator BaseIntel(IntelService<SocialMedia> v)
    {
        throw new NotImplementedException();
    }
}