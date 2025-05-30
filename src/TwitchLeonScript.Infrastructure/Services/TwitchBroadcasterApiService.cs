using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Domain.Options;

namespace TwitchLeonScript.Infrastructure.Services
{
    public class TwitchBroadcasterApiService(IOptions<TwitchOptions> options) : ITwitchBroadcasterApiService
    {
        public async Task<TwitchBroadcaster> GetBroadcasterAsync(string oauthToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);
            client.DefaultRequestHeaders.Add("Client-Id", options.Value.AppId);

            var response = await client.GetAsync("https://api.twitch.tv/helix/users").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
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
