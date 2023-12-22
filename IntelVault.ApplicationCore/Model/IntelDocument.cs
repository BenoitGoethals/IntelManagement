using IntelVault.Infrastructure.repos;
using MongoDB.Bson;

namespace IntelVault.ApplicationCore.Model;

public class IntelDocument:BaseIntel
{
  
    public DocumentType DocumentType { get; set; }
    public string? Description { get; set; }

    public string? LongDescription { get; set; }

    public string? FileName { get; set; }
  
    public byte[]? Content { get; set; }

    public DateTime? TimeCreated { get; set; }

    public List<string> Keywords { get; set; } = new List<string>();
    
    

}