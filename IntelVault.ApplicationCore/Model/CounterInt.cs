namespace IntelVault.ApplicationCore.Model;

/// <summary>
/// Counterintelligence (CounterIntel) report involves capturing information related to activities aimed at preventing or thwarting intelligence operations
/// </summary>
public class CounterInt
{
    // Operation Details
    public string OperationName { get; set; }
    public DateTime OperationStartDate { get; set; }
    public DateTime OperationEndDate { get; set; }

    // Targets and Suspects
    public List<string> Targets { get; set; }
    public List<string> Suspects { get; set; }

    // Tactics and Techniques
    public List<string> TacticsUsed { get; set; }
    public List<string> TechniquesUsed { get; set; }

    // Intelligence Sources
    public List<string> IntelligenceSources { get; set; }

    // Analysis and Findings
    public string Analysis { get; set; }
    public string Findings { get; set; }

    // Countermeasures and Recommendations
    public List<string> Countermeasures { get; set; }
    public List<string> Recommendations { get; set; }

    // Constructor
    public CounterInt()
    {
        // Initialize lists
        Targets = new List<string>();
        Suspects = new List<string>();
        TacticsUsed = new List<string>();
        TechniquesUsed = new List<string>();
        IntelligenceSources = new List<string>();
        Countermeasures = new List<string>();
        Recommendations = new List<string>();
        
    }
}