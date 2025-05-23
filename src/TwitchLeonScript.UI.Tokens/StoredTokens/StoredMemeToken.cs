namespace TwitchLeonScript.UI.Tokens.StoredTokens
{
    public sealed class StoredMemeToken
    {
        public required string OAuthToken { get; init; }
        public required string RefreshToken { get; init; }
        public required string BroadcasterId { get; init; }
        public required string BroadcasterName { get; init; }
        public required DateTime ExpiresAt { get; init; } = DateTime.UtcNow.AddHours(1);
    }
}
