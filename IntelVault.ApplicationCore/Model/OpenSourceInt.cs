using IntelVault.ApplicationCore.IntelData;
using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Model;
/// <summary>
/// Open-source intelligence (OSINT) are gathered from open sources. OSINT can be further segmented by the source type: Internet/General, Scientific/Technical, and various HUMINT specialties, e.g. trade shows, association meetings, and interviews.
/// </summary>
public class OpenSourceInt : BaseIntel
{
    // Source Information
    public string? SourceName { get; set; }
    public string? SourceType { get; set; }
    public string? SourceURL { get; set; }

    // Target Information
    public string? TargetName { get; set; }
    public string? TargetLocation { get; set; }
    public DateTime? ReportDate { get; set; }

    // Information Gathered
    public List<ListItem>? GatheredInformation { get; set; } = new List<ListItem>();

    // Analysis and Implications
    public string? Analysis { get; set; }
    public string? Implications { get; set; }

    // Recommendations
    public List<ListItem>? Recommendations { get; set; } = new List<ListItem>();


}