using TwitchLeonScript.Core.Meme.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace TwitchLeonScript.Core.Meme.Services
{
    public class MemeAuthService
    {
        public async Task<MemeBroadcaster> GetBroadcasterAsync(string oauthToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", oauthToken);

            var response = await client.GetAsync("https://memealerts.com/api/user/current");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            return new MemeBroadcaster
            {
                Id = root.GetProperty("id").GetString()!,
                Name = root.GetProperty("name").GetString()!
            };
        }
    }
}
