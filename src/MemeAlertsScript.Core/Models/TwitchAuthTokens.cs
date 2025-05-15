namespace MemeAlertsScript.Core.Models
{
    public class TwitchAuthTokens
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddHours(1);
        public string[]? Scope { get; set; }
        public string? TokenType { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    }
}
