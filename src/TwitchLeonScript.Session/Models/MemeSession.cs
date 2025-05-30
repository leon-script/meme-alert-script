namespace TwitchLeonScript.Session.Models
{
    public sealed class MemeSession
    {
        public required string OAuthToken { get; init; }
        public required string OAuthRefreshToken { get; init; }
        public required DateTime OAuthExpiresAt { get; init; } = DateTime.UtcNow.AddHours(1);
        public required string BroadcasterId { get; init; }
        public required string BroadcasterLogin { get; init; }
    }
}
