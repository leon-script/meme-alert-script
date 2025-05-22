using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MemeAlertsScript.Core.Meme.Services
{
    public class MemeBonusService
    {
        public async Task<bool> SendGivePointsAsync(string accessToken, string userId, string streamerId, int value)
        {
            var payload = new { userId, streamerId, value };
            var json = JsonSerializer.Serialize(payload);

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://memealerts.com/api/user/give-bonus", content);
            var result = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }
    }
}
