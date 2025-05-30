using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Infrastructure.Services
{
    public sealed class MemeSupporterApiService(
        IHttpClientFactory httpClientFactory,
        IMemeAuthContext authContext,
        JsonSerializerOptions jsonOptions)
        : IMemeSupporterApiService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MemeAlerts");

        public async Task<IEnumerable<MemeSupporter>> GetSupportersAsync(string query = "")
        {
            var payload = new { query };

            var json = JsonSerializer.Serialize(payload, jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            using var request = new HttpRequestMessage(HttpMethod.Post, "supporters") { Content = content };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authContext.OAuthToken);

            using var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using var document = await JsonDocument.ParseAsync(stream).ConfigureAwait(false);

            var dataElement = document.RootElement.GetProperty("data");
            var supporters = new List<MemeSupporter>();

            foreach (var element in dataElement.EnumerateArray())
            {
                supporters.Add(new MemeSupporter
                {
                    Id = element.GetProperty("supporterId").GetString()!,
                    Name = element.GetProperty("supporterName").GetString()!,
                });
            }

            return supporters;
        }
    }
}
