using System.Text.Json.Serialization;

namespace MemeAlertsScript.Core.Meme.Models
{
    public class MemeSupporter
    {
        [JsonPropertyName("supporterId")]
        public string Id { get; set; } = "";

        [JsonPropertyName("supporterName")]
        public string Name { get; set; } = "";
    }
}
