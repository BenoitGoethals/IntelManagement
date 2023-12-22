using IntelVault.Infrastructure.repos;
using MongoDB.Bson.Serialization.Attributes;

namespace IntelVault.ApplicationCore.Model;
[BsonDiscriminator("SocialMedia")]
public class SocialMedia : BaseIntel
{
    // User Information
    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
    public DateTime? AccountCreationDate { get; set; } = DateTime.Now;

    // Social Media Platform Details
    public string? Platform { get; set; }

    // Activity Metrics
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
    public int PostCount { get; set; }

    // Recent Posts
    public List<ListItem> RecentPosts { get; set; } = new();

    // Engagement Analysis
    public string? EngagementRate { get; set; }
    public string? SentimentAnalysis { get; set; }

    // Location Information
    public string? Location { get; set; }

    // Security and Privacy Settings
    public bool PrivateAccount { get; set; }
    public bool TwoFactorAuthentication { get; set; }

    public SocialMedia()
    {
        base.IntelType = TypeIntel.SocialMedia;
    }

    public override string ToString()
    {
        return $"{nameof(Username)}: {Username}, {nameof(DisplayName)}: {DisplayName}, {nameof(Bio)}: {Bio}, {nameof(AccountCreationDate)}: {AccountCreationDate}, {nameof(Platform)}: {Platform}, {nameof(FollowersCount)}: {FollowersCount}, {nameof(FollowingCount)}: {FollowingCount}, {nameof(PostCount)}: {PostCount}, {nameof(RecentPosts)}: {RecentPosts}, {nameof(EngagementRate)}: {EngagementRate}, {nameof(SentimentAnalysis)}: {SentimentAnalysis}, {nameof(Location)}: {Location}, {nameof(PrivateAccount)}: {PrivateAccount}, {nameof(TwoFactorAuthentication)}: {TwoFactorAuthentication}";
    }


}