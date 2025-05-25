using System.Text.Json.Serialization;

namespace TwitchLeonScript.Core.Meme.Models
{
    public class MemeSupporterDto
    {
        [JsonPropertyName("supporterId")]
        public required string Id { get; set; }

        [JsonPropertyName("supporterName")]
        public required string Name { get; set; }
    }
}
