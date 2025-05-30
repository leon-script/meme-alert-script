namespace TwitchLeonScript.Domain.Models
{
    public sealed class MemeOAuthToken
    {
        public required string Token { get; init; }
        public required string RefreshToken { get; init; }
        public required DateTime ExpiresAt { get; init; }
    }
}
