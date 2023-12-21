namespace IntelVault.ApplicationCore.Model;

public class ReportData
{
    public int Id { get; set; }
    public TypeIntel TypeBaseLine { get; set; }
    public long Count { get; set; }

    public string? Description { get; set; }
}