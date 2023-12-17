namespace IntelVault.ApplicationCore.Model;

public class ReportData
{
    public int Id { get; set; }
    public TypeIntel TypeBaseLine { get; set; }
    public int Count { get; set; }

    public string? Description { get; set; }
}