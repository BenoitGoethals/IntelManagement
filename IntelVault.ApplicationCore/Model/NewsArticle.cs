using System.Security.AccessControl;

namespace IntelVault.ApplicationCore.Model;

public class NewsArticle : BaseIntel
{
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? Author { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? Content { get; set; }
    public string? Source { get; set; }

    public List<string> keywords { get; set; }= new List<string>();
}