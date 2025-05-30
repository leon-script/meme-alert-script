using Microsoft.Extensions.Options;
using System.Text.Json;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Domain.Options;

namespace TwitchLeonScript.Infrastructure.Services
{
    public sealed class TwitchOAuthTokenApiService(IOptions<TwitchOptions> options) : ITwitchOAuthTokenApiService
    {
        public async Task<TwitchOAuthToken> GetOAuthTokenAsync(string code)
        {
            var parameters = new Dictionary<string, string>
            {
                { "client_id", options.Value.AppId },
                { "client_secret", options.Value.AppSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", options.Value.RedirectUri }
            };

            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(parameters);

            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var document = JsonDocument.Parse(json);

            return new TwitchOAuthToken
            {
                Token = document.RootElement.GetProperty("access_token").GetString()!,
                RefreshToken = document.RootElement.GetProperty("refresh_token").GetString()!,
                ExpiresAt = DateTime.UtcNow.AddSeconds(document.RootElement.GetProperty("expires_in").GetInt32()),
            };
        }
    }
}
