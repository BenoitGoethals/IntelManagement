namespace IntelVault.ApplicationCore.Model;

/// <summary>
/// Geospatial intelligence (GEOINT) are gathered from satellite and aerial photography, or mapping/terrain data.
/// </summary>
public class GeoInt
{
    // Location Information
    public string TargetLocation { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Imagery and Maps
    public List<string> ImagerySources { get; set; }
    public List<string> Maps { get; set; }

    // Terrain and Infrastructure Details
    public string TerrainDescription { get; set; }
    public List<string> InfrastructureDetails { get; set; }

    // Environmental Factors
    public string WeatherConditions { get; set; }
    public string NaturalHazards { get; set; }

    // Analysis and Interpretation
    public string Analysis { get; set; }
    public string Interpretation { get; set; }

    // Recommendations
    public List<string> Recommendations { get; set; }

    // Constructor
    public GeoInt()
    {
        // Initialize lists
        ImagerySources = new List<string>();
        Maps = new List<string>();
        InfrastructureDetails = new List<string>();
        Recommendations = new List<string>();
    }
}