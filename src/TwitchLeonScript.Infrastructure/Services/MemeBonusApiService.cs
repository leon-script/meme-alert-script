using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TwitchLeonScript.Domain.Abstractions;

namespace TwitchLeonScript.Infrastructure.Services
{
    public class MemeBonusApiService(
        IHttpClientFactory httpClientFactory,
        JsonSerializerOptions jsonOptions,
        IMemeAuthContext authContext)
        : IMemeBonusApiService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MemeAlerts");

        public async Task<HttpStatusCode> GivePointsAsync(string userId, int value)
        {
            if (!authContext.IsLogged)
            {
                throw new InvalidOperationException("Meme user is not logged in. Please log in to access this resource.");
            }

            var streamerId = authContext.BroadcasterId;
            var payload = new { userId, streamerId, value };

            var json = JsonSerializer.Serialize(payload, jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            
            using var request = new HttpRequestMessage(HttpMethod.Post, "user/give-bonus") { Content = content };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authContext.OAuthToken);

            using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }
    }
}
