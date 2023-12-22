
namespace IntelVault.ApplicationCore.Model;

public class Intel
{
    public int Id { get; set; }

    private List<GeneralIntel> IntelReports { get; set; } =new List<GeneralIntel>();

    public required string ShortDescription { get; set; }
    public required string Name { get; set; }

    public required DateTime DtgInjected { get; set; }

    public string? LongDescription { get; set; }

    public OperatorIntel? OperatorIntel { get; set; }


    public void Add(GeneralIntel intelAsset)
    {
        IntelReports.Add(intelAsset);
    }

    public void Remove(GeneralIntel intelAsset) {  IntelReports.Remove(intelAsset); }

    public void Clear() { IntelReports.Clear();}

    public bool Contains(GeneralIntel intelAsset) {  return IntelReports.Contains(intelAsset);}

    
}