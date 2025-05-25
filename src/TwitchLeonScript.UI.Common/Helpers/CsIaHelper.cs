using System.Text.Json;

namespace TwitchLeonScript.UI.Common.Helpers
{
    public static class CsIaHelper
    {
        public static bool IsCsIaTrue(string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return false;
            }

            using var document = JsonDocument.Parse(rawInput);
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
                return !string.IsNullOrEmpty(innerJson) && IsCsIaTrue(innerJson);
            }

            return false;
        }
    }
}
