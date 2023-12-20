using IntelVault.ApplicationCore.IntelData;

namespace IntelVault.ApplicationCore.Model;

public class Informant:BaseIntel
{
    // Personal Information
    public string? InformantName { get; set; }
    public string? InformantCodeName { get; set; }
    public int? Age { get; set; } = 20;
    public Gender Gender { get; set; } = Gender.Male;

    // Contact Information
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    // Informant Role and Background
    public string? InformantRole { get; set; }
    public string? BackgroundInfo { get; set; }

    // Reliability and Trustworthiness
    public int? ReliabilityRating { get; set; } = 1;// You can use a scale (e.g., 1 to 10)

    // Intelligence Contributions
    public string? IntelProvided { get; set; }
    public string? AreasOfExpertise { get; set; }

    // Operational Status
    public bool? ActiveStatus { get; set; }
    public DateTime? LastContactDate { get; set; }=DateTime.Now;

    public Informant()
    {
        base.IntelType  = TypeIntel.Informant;
    }
}