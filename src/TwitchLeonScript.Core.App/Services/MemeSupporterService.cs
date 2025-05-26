using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Services
{
    public sealed class MemeSupporterService(
        IHttpClientFactory httpClientFactory,
        JsonSerializerOptions jsonOptions)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MemeAlerts");

        public async Task<List<MemeSupporterDto>> GetSupportersAsync(string accessToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "supporters")
            {
                Content = new StringContent(string.Empty)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json) }
                }
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(stream);

            var dataElement = document.RootElement.GetProperty("data");
            var supporters = JsonSerializer.Deserialize<List<MemeSupporterDto>>(dataElement.GetRawText(), jsonOptions);

            return supporters ?? [];
        }
    }
}
