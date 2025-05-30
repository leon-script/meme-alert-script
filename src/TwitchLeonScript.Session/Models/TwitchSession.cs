namespace TwitchLeonScript.Session.Models
{
    public sealed class TwitchSession
    {
        public required string AppToken { get; init; }
        public required string OAuthToken { get; init; }
        public required string OAuthRefreshToken { get; init; }
        public required DateTime OAuthExpiresAt { get; init; }
        public required string BroadcasterId { get; init; }
        public required string BroadcasterLogin { get; init; }
    }
}
