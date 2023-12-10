namespace IntelVault.ApplicationCore.Model;
/// <summary>
/// Technical intelligence (TECHINT) are gathered from analysis of weapons and equipment used by the armed forces of foreign nations, or environmental conditions.
/// 
/// Medical intelligence (MEDINT) – gathered from analysis of medical records and/or actual physiological examinations to determine health and/or particular ailments and allergic conditions for consideration
/// </summary>
public class TechInt
{
    // Technology Details
    public string TechnologyName { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string TechnicalSpecifications { get; set; }

    // Acquisition Information
    public string AcquisitionSource { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public decimal Cost { get; set; }

    // Usage and Performance
    public string UsageHistory { get; set; }
    public string PerformanceAnalysis { get; set; }

    // Compatibility and Integration
    public List<string> CompatibleTechnologies { get; set; }
    public string IntegrationDetails { get; set; }

    // Analysis and Findings
    public string Analysis { get; set; }
    public string Findings { get; set; }

    // Recommendations
    public List<string> Recommendations { get; set; }

    // Constructor
    public TechInt()
    {
        // Initialize lists
        CompatibleTechnologies = new List<string>();
        Recommendations = new List<string>();
    }
}