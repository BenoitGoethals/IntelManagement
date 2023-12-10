using IntelVault.ApplicationCore.IntelData;
using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Model;

/// <summary>
/// Cyber intelligence (CYBINT), sometimes called "digital network intelligence (DNINT)", are gathered from cyberspace. CYBINT can be considered as a subset of OSINT.[2]
/// "Cyberint" typically refers to Cyber Intelligence, which is the practice of collecting,
/// analyzing, and leveraging information about digital threats and vulnerabilities to protect an organization's assets,
/// networks, and systems from cyber threats. This field involves monitoring online activities, analyzing data, and using intelligence to proactively defend against cyber attacks.
/// </summary>
public class CybInt : BaseIntel
{
    // Incident Details
    public TypeOfCybInt IncidentType { get; set; } = TypeOfCybInt.EmailPhishing;

    public string IncidentDescription { get; set; }

    // Attribution
    public string Attribution { get; set; }

    // Tactics, Techniques, and Procedures
    public List<ListItem> TtPs { get; set; } = new();

    // Vulnerabilities Exploited
    public List<ListItem> ExploitedVulnerabilities { get; set; } = new();

    // Impact Assessment
    public string? ImpactAssessment { get; set; } = "Test";

    // Mitigation Recommendations
    public List<ListItem> MitigationRecommendations { get; set; } = new();

    // Timeline of Events
    public List<ListItemDate> Timeline { get; set; } = new();

    // Indicators of Compromise (IOCs)
    public List<ListItem> IndicatorsOfCompromise { get; set; } = new();

    // Analysis of Malware or Malicious Code
    public string? MalwareAnalysis { get; set; } = "test";

    // Recommendations for Future Prevention
    public List<ListItem> PreventionRecommendations { get; set; } = new();



}