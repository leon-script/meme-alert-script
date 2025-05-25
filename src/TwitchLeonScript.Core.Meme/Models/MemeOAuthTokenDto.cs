using System.Text.Json.Serialization;

namespace TwitchLeonScript.Core.Meme.Models
{
    public class MemeOAuthTokenDto
    {
        [JsonPropertyName("accessToken")] 
        public required string AccessToken { get; init; }

        [JsonPropertyName("refreshToken")]
        public required string RefreshToken { get; init; }
    }
}
