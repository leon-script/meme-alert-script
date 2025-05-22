namespace TwitchLeonScript.WinForms.Tokens.StoredTokens
{
    public sealed class StoredTwitchToken
    {
        public required string AccessToken { get; init; }
        public required string OAuthToken { get; init; }
        public required string RefreshToken { get; init; }
        public required string BroadcasterId { get; init; }
        public required string BroadcasterName { get; init; }
        public required DateTime ExpiresAt { get; init; }
    }
}
