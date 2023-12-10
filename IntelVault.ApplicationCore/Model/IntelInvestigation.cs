using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Model;

public class IntelInvestigationFile
{
    // General Information
    public string CaseNumber { get; set; }
    public DateTime IncidentDate { get; set; }
    public string IncidentLocation { get; set; }

    //// Parties Involved
    //public string SuspectName { get; set; }
    //public string SuspectAddress { get; set; }
    //public string VictimName { get; set; }
    //public string VictimAddress { get; set; }

    // Incident Details
    public string IncidentDescription { get; set; }
    public List<BaseIntel> EvidenceCollected { get; set; }

    public List<IntelDocument> IntelDocuments = new();

    // Investigative Team
    public string InvestigatorName { get; set; }
    public string InvestigatorBadgeNumber { get; set; }

    // Status and Conclusion
    public string InvestigationStatus { get; set; }
    public string Conclusion { get; set; }

    // Methods for Additional Actions (e.g., data validation, processing)
    public void ValidateData()
    {
        // Add data validation logic here
    }

    // Additional Methods as Needed


}