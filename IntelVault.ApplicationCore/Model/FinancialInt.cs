namespace IntelVault.ApplicationCore.Model;
/// <summary>
/// Financial intelligence (FININT) are gathered from analysis of monetary transactions.
/// </summary>
public class FinancialInt
{
    // Transaction Details
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Counterparty { get; set; }

    // Parties Involved
    public List<string> InvolvedParties { get; set; }

    // Suspicious Activity
    public string SuspiciousActivityDescription { get; set; }

    // Compliance and Regulatory Information
    public string RegulatoryComplianceStatus { get; set; }

    // Investigation Details
    public string InvestigatingAgency { get; set; }
    public DateTime InvestigationStartDate { get; set; }
    public string InvestigationStatus { get; set; }

    // Findings and Recommendations
    public string InvestigationFindings { get; set; }
    public List<string> Recommendations { get; set; }

    // Constructor
    public FinancialInt()
    {
        // Initialize lists
        InvolvedParties = new List<string>();
        Recommendations = new List<string>();
    }
}