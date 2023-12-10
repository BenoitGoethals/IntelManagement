namespace IntelVault.ApplicationCore.Model;

public class GovSource
{
    // Source Information
    public string SourceName { get; set; }
    public string SourceType { get; set; }
    public string SourceAgency { get; set; }

    // Reliability and Clearance Level
    public int ReliabilityRating { get; set; } // You can use a scale (e.g., 1 to 10)
    public string ClearanceLevel { get; set; }

    // Contact Information
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public string ContactPhoneNumber { get; set; }
    public string ContactEmail { get; set; }

    // Intelligence Contributions
    public List<string> IntelligenceContributions { get; set; }

    // Operational Details
    public string OperationalStatus { get; set; }
    public DateTime LastContactDate { get; set; }

    // Constructor
    public GovSource()
    {
        // Initialize lists
        IntelligenceContributions = new List<string>();
    }
}