using TwitchLeonScript.Core.Meme.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace TwitchLeonScript.Core.Meme.Services
{
    public class MemeSupporterService
    {
        public async Task<List<MemeSupporter>> GetSupportersAsync(string accessToken)
        {
            using var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://memealerts.com/api/supporters");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = new StringContent("");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = await JsonSerializer.DeserializeAsync<MemeSupporterListResponse>(stream, options);

            return result?.Data ?? new List<MemeSupporter>();
        }

        internal class MemeSupporterListResponse
        {
            public List<MemeSupporter> Data { get; set; } = new();
        }
    }
}
