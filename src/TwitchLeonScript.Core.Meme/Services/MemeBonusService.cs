using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TwitchLeonScript.Core.Meme.Models;

namespace TwitchLeonScript.Core.Meme.Services
{
    public class MemeBonusService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public MemeBonusService(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonOptions)
        {
            _httpClient = httpClientFactory.CreateClient("MemeAlerts");
            _jsonOptions = jsonOptions;
        }

        public async Task<HttpStatusCode> SendGivePointsAsync(string oauthToken, MemeBonusDto memeBonus)
        {
            var json = JsonSerializer.Serialize(memeBonus, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            
            using var request = new HttpRequestMessage(HttpMethod.Post, "user/give-bonus") { Content = content };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }
    }
}
