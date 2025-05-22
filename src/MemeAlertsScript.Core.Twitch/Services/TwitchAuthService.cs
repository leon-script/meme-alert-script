using MemeAlertsScript.Core.Common.Options;
using MemeAlertsScript.Core.Twitch.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MemeAlertsScript.Core.Twitch.Services
{
    public class TwitchAuthService
    {
        private readonly TwitchOptions _options;

        public TwitchAuthService(IOptions<TwitchOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string?> GetAppTokenAsync()
        {
            using var http = new HttpClient();
            var response = await http.PostAsync(
                $"https://id.twitch.tv/oauth2/token" +
                $"?client_id={_options.AppId}" +
                $"&client_secret={_options.AppSecret}" +
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

        public async Task<TwitchOAuthToken?> GetOAuthTokenAsync(string code)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", _options.AppId },
                { "client_secret", _options.AppSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _options.RedirectUri }
            };

            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(parameters);

            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(json);

            return new TwitchOAuthToken
            {
                AccessToken = document.RootElement.GetProperty("access_token").GetString()!,
                RefreshToken = document.RootElement.GetProperty("refresh_token").GetString()!,
                ExpiresAt = DateTime.UtcNow.AddSeconds(document.RootElement.GetProperty("expires_in").GetInt32()),
                Scope = document.RootElement.GetProperty("scope").EnumerateArray().Select(x => x.GetString()!).ToArray(),
                TokenType = document.RootElement.GetProperty("token_type").GetString()!
            };
        }

        public async Task<TwitchBroadcaster> GetBroadcasterAsync(string oauthToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
            client.DefaultRequestHeaders.Add("Client-Id", _options.AppId);

            var response = await client.GetAsync("https://api.twitch.tv/helix/users");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(json);
            var user = document.RootElement.GetProperty("data")[0];

            return new TwitchBroadcaster
            {
                Id = user.GetProperty("id").GetString()!,
                Login = user.GetProperty("login").GetString()!,
            };
        }
    }
}
