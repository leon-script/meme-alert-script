using System.Text.Json;
using TwitchLeonScript.Domain.Abstractions;

namespace TwitchLeonScript.Infrastructure.Services
{
    public sealed class MemeStateService : IMemeStateService
    {
        public bool IsCsIaAuth(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }

            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            if (root.ValueKind == JsonValueKind.Object)
            {
                if (root.TryGetProperty("cs-ia", out var csIaProp))
                {
                    return csIaProp.GetString()?.ToLowerInvariant() == "true";
                }
            }

            if (root.ValueKind == JsonValueKind.String)
            {
                var innerJson = root.GetString();
                return !string.IsNullOrEmpty(innerJson) && IsCsIaAuth(innerJson);
            }

            return false;
        }
    }
}
