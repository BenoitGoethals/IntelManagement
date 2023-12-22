using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Model;

public class PersonOfInterest : BaseIntel
{
    // Personal Information
    public string Name { get; set; }
    public string Alias { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public string? Nationality { get; set; } = "BE";

    // Physical Attributes
    public string Height { get; set; }
    public string Weight { get; set; }
    public string EyeColor { get; set; }
    public string HairColor { get; set; }

    // Contact Information
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    // Known Associations
    public string Affiliations { get; set; }
    public string Relationships { get; set; }

    // Criminal Record or Suspicious Activities
    public string CriminalRecord { get; set; }
    public string SuspiciousActivities { get; set; }

    // Level of Threat or Interest
    public int ThreatLevel { get; set; } // You can use a scale (e.g., 1 to 10)

    // Additional Notes or Comments
    public string Notes { get; set; }

    //Political 
    public string PoliticalGroup { get; set; }
    public List<ListItemPhoto> Pictures { get; set; } = new List<ListItemPhoto>();

    // Constructor
    public PersonOfInterest()
    {
        base.IntelType = TypeIntel.Other;
    }
}