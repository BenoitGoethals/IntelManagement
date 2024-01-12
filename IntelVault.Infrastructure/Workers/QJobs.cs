namespace IntelVault.Infrastructure.Workers;

public class QJobs
{
    public List<string?> Keywords { get; set; } = new List<string?>();
    public Guid Id { get;  set; } = new Guid();
    public string? Name { get; set; }
    public string? Group { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Next { get; set; }

    public int Interval { get; set; }
    public string? Url { get; set; }
    public OpenSourceType OpenSourceType { get; set; } = OpenSourceType.Scrapper;

    protected bool Equals(QJobs other)
    {
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((QJobs)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();


    }

    public override string ToString()
    {
        return
            $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(StartDate)}: {StartDate}, {nameof(EndDate)}: {EndDate}, {nameof(Next)}: {Next}";
    }
}
   