namespace IntelVault.ApplicationCore.Model;

public class ImInt
{
    // Image Details
    public string ImageName { get; set; }
    public string ImageSource { get; set; }
    public DateTime CaptureDate { get; set; }
    public string ImageFormat { get; set; }

    // Target Information
    public string TargetName { get; set; }
    public string TargetLocation { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Image Analysis
    public string ImageAnalysis { get; set; }

    // Key Features
    public List<string> KeyFeatures { get; set; }

    // Interpretation and Findings
    public string Interpretation { get; set; }
    public string Findings { get; set; }

    // Recommendations
    public List<string> Recommendations { get; set; }

    // Constructor
    public ImInt()
    {
        // Initialize lists
        KeyFeatures = new List<string>();
        Recommendations = new List<string>();
    }
}