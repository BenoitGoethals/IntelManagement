using IntelVault.ApplicationCore.IntelData;
using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Model;
/// <summary>
/// Base Class
/// </summary>
public class GeneralIntel : BaseIntel
{

    public string Name { get; set; }

    public DateTime? DtgInjected { get; set; }

    public DateTime? DtgOccurrence { get; set; }

    public HumanSource HumanSource { get; set; } = new HumanSource();

    public List<ListItem> AdditionalInfo { get; set; } = new List<ListItem>();

    // General Information
    public string ReportTitle { get; set; }
    public DateTime? ReportDate { get; set; }
    public string ReportingAgency { get; set; }

    // Incident Details
    public TypeIntel IncidentType { get; set; }
    public string IncidentLocation { get; set; }
    public DateTime? IncidentDateTime { get; set; }

    // Key Players or Entities
    public List<ListItem> KeyPlayers { get; set; }

    // Intelligence Sources
    public List<ListItem> IntelligenceSources { get; set; }

    // Analysis and Findings
    public string Analysis { get; set; }
    public string Findings { get; set; }

    // Recommendations
    public List<ListItem> Recommendations { get; set; }

    // Constructor
    public GeneralIntel()
    {
        // Initialize lists
        KeyPlayers = new List<ListItem>();
        IntelligenceSources = new List<ListItem>();
        Recommendations = new List<ListItem>();
    }


}