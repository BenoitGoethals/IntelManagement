using MongoDB.Bson;

namespace IntelVault.Infrastructure.repos;

public class MongoEntity
{
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public DateTime CreatedDtg { get; set; } = DateTime.UtcNow;

    public string? IntelVaultUser { get; set; }


    protected bool Equals(MongoEntity other)
    {
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((MongoEntity)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(CreatedDtg)}: {CreatedDtg}";
    }
}