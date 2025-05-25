using System.Net.Http.Headers;
using System.Text.Json;
using TwitchLeonScript.Core.Meme.Models;

namespace TwitchLeonScript.Core.Meme.Services
{
    public sealed class MemeBroadcasterService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public MemeBroadcasterService(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonOptions)
        {
            _httpClient = httpClientFactory.CreateClient("MemeAlerts");
            _jsonOptions = jsonOptions;
        }

        public async Task<MemeBroadcasterDto?> GetBroadcasterAsync(string oauthToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "user/current");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(stream);
            
            var dataElement = document.RootElement;
            var supporter = JsonSerializer.Deserialize<MemeBroadcasterDto>(dataElement.GetRawText(), _jsonOptions);

            return supporter;
        }
    }
}
