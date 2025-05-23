namespace TwitchLeonScript.Core.Meme.Models
{
    public class MemeOAuthToken
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}
