using System.Net.Http.Headers;
using System.Text.Json;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Infrastructure.Services
{
    public sealed class MemeBroadcasterApiService(IHttpClientFactory httpClientFactory) : IMemeBroadcasterApiService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MemeAlerts");

        public async Task<MemeBroadcaster> GetBroadcasterAsync(string oauthToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "user/current");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);

            using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            using var json = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var document = await JsonDocument.ParseAsync(json).ConfigureAwait(false);

            return new MemeBroadcaster
            {
                Id = document.RootElement.GetProperty("id").GetString()!,
                Login = document.RootElement.GetProperty("name").GetString()!,
            };
        }
    }
}
