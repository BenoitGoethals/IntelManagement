namespace IntelVault.ApplicationCore.Model;

public class HumanSource
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ForName { get; set; }

    public List<string> AdditionalInfo { get; set; } = new List<string>();

    public DateTime DtgInjected { get; set; } = DateTime.MinValue;


    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(ForName)}: {ForName}";
    }
}