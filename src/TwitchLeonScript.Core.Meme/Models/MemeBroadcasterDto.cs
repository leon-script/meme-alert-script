using System.Text.Json.Serialization;

namespace TwitchLeonScript.Core.Meme.Models
{
    public class MemeBroadcasterDto
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }
}
