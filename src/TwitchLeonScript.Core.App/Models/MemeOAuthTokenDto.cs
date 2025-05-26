using System.Text.Json.Serialization;

namespace TwitchLeonScript.Core.App.Models
{
    public class MemeOAuthTokenDto
    {
        [JsonPropertyName("accessToken")] 
        public required string AccessToken { get; init; }

        [JsonPropertyName("refreshToken")]
        public required string RefreshToken { get; init; }
    }
}
