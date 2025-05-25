using System.Text.Json.Serialization;

namespace TwitchLeonScript.Core.Meme.Models
{
    public sealed class MemeBonusDto
    {
        [JsonPropertyName("userId")]
        public required string UserId { get; init; }

        [JsonPropertyName("streamerId")]
        public required string BroadcasterId { get; init; }

        [JsonPropertyName("value")]
        public required int Value { get; init; }
    }
}
