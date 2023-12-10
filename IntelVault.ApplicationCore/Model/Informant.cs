﻿namespace IntelVault.ApplicationCore.Model;

public class Informant
{
    public int Id { get; set; }
    // Personal Information
    public string InformantName { get; set; }
    public string InformantCodeName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    // Contact Information
    public string ContactNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    // Informant Role and Background
    public string InformantRole { get; set; }
    public string BackgroundInfo { get; set; }

    // Reliability and Trustworthiness
    public int ReliabilityRating { get; set; } // You can use a scale (e.g., 1 to 10)

    // Intelligence Contributions
    public string IntelProvided { get; set; }
    public string AreasOfExpertise { get; set; }

    // Operational Status
    public bool ActiveStatus { get; set; }
    public DateTime LastContactDate { get; set; }


}