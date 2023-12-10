namespace IntelVault.ApplicationCore.Model;

public class SitRep
{
    // General Information
    public DateTime ReportDateTime { get; set; }
    public string ReportingUnit { get; set; }
    public string Location { get; set; }

    // Enemy Forces
    public string EnemyDisposition { get; set; }
    public string EnemyCapabilities { get; set; }

    // Friendly Forces
    public string FriendlyDisposition { get; set; }
    public string FriendlyCapabilities { get; set; }

    // Civilian Situation
    public string CivilianConsiderations { get; set; }

    // Weather and Terrain
    public string WeatherConditions { get; set; }
    public string TerrainAnalysis { get; set; }

    // Other Relevant Information
    public List<string> OtherInformation { get; set; }

    // Recommendations and Plans
    public string Recommendations { get; set; }
    public string FuturePlans { get; set; }

    // Constructor
    public SitRep()
    {
        // Initialize lists
        OtherInformation = new List<string>();
    }
}