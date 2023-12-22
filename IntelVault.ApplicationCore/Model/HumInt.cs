using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Model;
/// <summary>
/// "HUMINT" stands for Human Intelligence, and it refers to the gathering of information through human sources, such as spies,
/// informants, and other individuals. 
/// </summary>
public class HumInt : BaseIntel
{
    //Details about the person or entity providing the information, including their identity, reliability, and access to the information.
    public string SourceInformation { get; set; }

    public HumIntType HumIntType { get; set; }

    //Information about the circumstances surrounding the intelligence, providing context to better understand its significance.
    public string ContextBackground { get; set; }

    //When and where the information was obtained, helping to assess its relevance and potential timeliness.
    public string TimeLocation { get; set; }

    //An evaluation of the source's trustworthiness and the likelihood that the information is accurate.
    public string ReliabilityCredibility { get; set; }

    //Specific details provided in the report and an assessment of their accuracy.
    public string AccuracyOfDetails { get; set; }

    // Interpretation of the information, including its potential implications, significance, and any potential biases.
    public string AssessmentAndAnalysis { get; set; }

    //Information on how the report should be classified, disseminated, and handled to protect sensitive information.
    public string ClassificationHandlingInstructions { get; set; }
    // Source Information
    public string SourceName { get; set; }
    public string SourceType { get; set; }
    public string SourceAffiliation { get; set; }

    // Reliability and Access Level
    public int ReliabilityRating { get; set; } // You can use a scale (e.g., 1 to 10)
    public string AccessLevel { get; set; }

    // Contact Information
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public string ContactPhoneNumber { get; set; }
    public string ContactEmail { get; set; }

    // Intelligence Details
    public List<ListItem> IntelligenceDetails { get; set; }

    // Operational Details
    public string OperationalStatus { get; set; }
    public DateTime? LastContactDate { get; set; } = DateTime.Now;

    // Constructor
    public HumInt()
    {
        // Initialize lists
        IntelligenceDetails = new List<ListItem>();
        base.IntelType = TypeIntel.Humint;
    }
}
