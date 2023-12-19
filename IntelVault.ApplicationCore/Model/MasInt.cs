namespace IntelVault.ApplicationCore.Model;

/// <summary>
/// Measurement and Signature
/// Measurement and signature intelligence (MASINT) are gathered from an array of signatures (distinctive characteristics) of fixed or dynamic target sources.
/// According to the Air Force Institute of Technology's Center for MASINT Studies and Research,
/// MASINT is split into six major disciplines: electro-optical, nuclear, radar, geophysical, materials, and radiofrequency.[3]
/// </summary>
public class MasInt
{
    public MasIntType MasIntType { get; set; }
    public string MeasurementType { get; set; }
    public double MeasurementValue { get; set; }
    public string MeasurementUnit { get; set; }

    // Signature Analysis
    public string SignatureAnalysis { get; set; }

    // Target Information
    public string TargetName { get; set; }
    public string TargetLocation { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Environmental Factors
    public string WeatherConditions { get; set; }
    public string AtmosphericConditions { get; set; }

    // Analysis and Findings
    public string Analysis { get; set; }
    public string Findings { get; set; }

    // Recommendations
    public List<string> Recommendations { get; set; }

    // Constructor
    public MasInt()
    {
        // Initialize lists
        Recommendations = new List<string>();
    }
}