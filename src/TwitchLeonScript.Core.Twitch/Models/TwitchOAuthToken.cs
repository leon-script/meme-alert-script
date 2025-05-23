namespace TwitchLeonScript.Core.Twitch.Models
{
    public class TwitchOAuthToken
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required string[] Scope { get; set; }
        public required string TokenType { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(1);
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    }
}
