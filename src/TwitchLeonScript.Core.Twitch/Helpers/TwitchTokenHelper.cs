using System.Text.Json;

namespace TwitchLeonScript.Core.Twitch.Helpers
{
    public class TwitchTokenHelper
    {
        public static async Task<string?> GetAppTokenAsync(string clientId, string clientSecret)
        {
            using var http = new HttpClient();
            var response = await http.PostAsync(
                $"https://id.twitch.tv/oauth2/token" +
                $"?client_id={clientId}" +
                $"&client_secret={clientSecret}" +
                $"&grant_type=client_credentials",
                null);

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(json);

            if (document.RootElement.TryGetProperty("access_token", out JsonElement accessTokenElement))
            {
                return accessTokenElement.GetString();
            }

            return null;
        }
    }
}
