using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Services
{
    public sealed class MemeBroadcasterService(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonOptions)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MemeAlerts");

        public async Task<MemeBroadcasterDto?> GetBroadcasterAsync(string oauthToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "user/current");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(stream);

            var dataElement = document.RootElement;
            var supporter = JsonSerializer.Deserialize<MemeBroadcasterDto>(dataElement.GetRawText(), jsonOptions);

            return supporter;
        }
    }
}
