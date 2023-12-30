namespace IntelVault.Worker.model;

public class OpenSourceRequest
{
    public Guid Id { get; set; }
    public OpenSourceType SourceType { get; set; }
    public required string Url { get; set; }
    public required List<string> KeyWords { get; set; }

    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    protected bool Equals(OpenSourceRequest other)
    {
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((OpenSourceRequest) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(SourceType)}: {SourceType}, {nameof(Url)}: {Url}, {nameof(KeyWords)}: {KeyWords}, {nameof(Start)}: {Start}, {nameof(End)}: {End}";
    }
    
}