using MemeAlertsScript.Core.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MemeAlertsScript.Twitch
{
    public class TwitchOAuthHelper
    {
        public static string CreateOAuthState()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static Uri CreateOAuthUrl(string clientId, string redirectUri, string[] scopes, string state)
        {
            return new Uri($"https://id.twitch.tv/oauth2/authorize" +
                           $"?client_id={clientId}" +
                           $"&redirect_uri={redirectUri}" +
                           $"&response_type=code" +
                           $"&scope={string.Join("+", scopes)}" +
                           $"&state={state}");
        }

        public static async Task<TwitchBroadcaster> GetTwitchUserInfoAsync(string clientId, string accessToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Add("Client-Id", clientId);

            var response = await client.GetAsync("https://api.twitch.tv/helix/users");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(json);
            var user = document.RootElement.GetProperty("data")[0];

            return new TwitchBroadcaster
            {
                Id = user.GetProperty("id").GetString()!,
                Login = user.GetProperty("login").GetString()!,
                //DisplayName = user.GetProperty("display_name").GetString()!,
                //Type = user.GetProperty("type").GetString()!,
                //Description = user.GetProperty("description").GetString()!,
                //ProfileImageUrl = user.GetProperty("profile_image_url").GetString()!,
                //OfflineImageUrl = user.GetProperty("offline_image_url").GetString()!,
                //ViewCount = user.GetProperty("view_count").GetInt32(),
                //CreatedAt = DateTime.Parse(user.GetProperty("created_at").GetString()!)
            };
        }
    }
}
