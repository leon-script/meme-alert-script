using System.Text.Json;

namespace MemeAlertsScript.Twitch
{
    public class TwitchApiHelper
    {
        public static async Task<string?> GetAppTokenAsync(string appId, string appSecret)
        {
            using var http = new HttpClient();
            var response = await http.PostAsync(
                $"https://id.twitch.tv/oauth2/token" +
                $"?client_id={appId}" +
                $"&client_secret={appSecret}" +
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
