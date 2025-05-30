using Microsoft.Extensions.Options;
using System.Text.Json;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Domain.Options;

namespace TwitchLeonScript.Infrastructure.Services
{
    public sealed class TwitchAppTokenApiService(IOptions<TwitchOptions> options) : ITwitchAppTokenApiService
    {
        public async Task<TwitchAppToken> GetAppTokenAsync()
        {
            using var client = new HttpClient();

            var response = await client.PostAsync(
                $"https://id.twitch.tv/oauth2/token" +
                $"?client_id={options.Value.AppId}" +
                $"&client_secret={options.Value.AppSecret}" +
                $"&grant_type=client_credentials",
                null).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var document = JsonDocument.Parse(json);

            return new TwitchAppToken
            {
                Token = document.RootElement.GetProperty("access_token").GetString()!,
            };
        }
    }
}
