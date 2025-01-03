﻿using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Model;

public class IntelInvestigationFile:BaseIntel
{
    // General Information
    public string? CaseNumber { get; set; }
    public DateTime? StartedDateTimeDate { get; set; }
    public DateTime? EndedDateTimeDate { get; set; }
    public string? Description { get; set; }
    public List<PersonOfInterest>? PersonOfInterests { get; set; } = new List<PersonOfInterest>();

    // Incident Details
    

    public string? LongDescription { get; set; }
    public List<BaseIntel>? EvidenceCollected { get; set; } = new List<BaseIntel>();

    public List<IntelDocument> IntelDocuments = new();

    // Investigative Team
    public string? InvestigatorName { get; set; }
    public string? InvestigatorBadgeNumber { get; set; }
    public string? InvestigatorNote { get; set; }
    public List<ListItem>? ExpertOpinions { get; set; } = new List<ListItem>();

    // Status and Conclusion
    public InvestigationStatus? InvestigationStatus { get; set; } = Model.InvestigationStatus.Init;
    public string? Conclusion { get; set; }

    // Methods for Additional Actions (e.g., data validation, processing)
    public void ValidateData()
    {
        // Add data validation logic here
    }

    // Additional Methods as Needed


}