namespace IntelVault.ApplicationCore.Model;
/// <summary>
/// Medical intelligence (MEDINT) – gathered from analysis of medical records and/or actual physiological examinations to determine health
/// and/or particular ailments and allergic conditions for consideration
/// </summary>
public class MedInt
{
    // Patient Information
    public string PatientName { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    // Medical Condition Details
    public string Diagnosis { get; set; }
    public List<string> Symptoms { get; set; }
    public List<string> Treatments { get; set; }

    // Medical Facility Information
    public string FacilityName { get; set; }
    public string FacilityLocation { get; set; }

    // Healthcare Provider Details
    public string ProviderName { get; set; }
    public string ProviderContact { get; set; }

    // Analysis and Findings
    public string Analysis { get; set; }
    public string Findings { get; set; }

    // Recommendations
    public List<string> Recommendations { get; set; }

    // Constructor
    public MedInt()
    {
        // Initialize lists
        Symptoms = new List<string>();
        Treatments = new List<string>();
        Recommendations = new List<string>();
    }
}