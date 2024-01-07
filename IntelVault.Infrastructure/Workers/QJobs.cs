namespace IntelVault.Infrastructure.Workers;

public class QJobs
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Next { get; set; }
}
   