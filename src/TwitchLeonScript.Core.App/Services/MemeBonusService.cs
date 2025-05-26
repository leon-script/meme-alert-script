using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Services
{
    public class MemeBonusService(
        IHttpClientFactory httpClientFactory,
        JsonSerializerOptions jsonOptions)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MemeAlerts");

        public async Task<HttpStatusCode> SendGivePointsAsync(string oauthToken, MemeBonusDto memeBonus)
        {
            var json = JsonSerializer.Serialize(memeBonus, jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            
            using var request = new HttpRequestMessage(HttpMethod.Post, "user/give-bonus") { Content = content };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }
    }
}
