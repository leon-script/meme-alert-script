using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MemeAlertsScripts.Twitch
{
    public class AuthApi
    {
        public static async Task<string?> GetAppAccessTokenAsync(string clientId, string clientSecret)
        {
            using var http = new HttpClient();
            var response = await http.PostAsync(
                $"https://id.twitch.tv/oauth2/token" +
                $"?client_id={clientId}" +
                $"&client_secret={clientSecret}" +
                $"&grant_type=client_credentials",
                null);

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(json);

            if (document.RootElement.TryGetProperty("access_token", out JsonElement accessTokenElement))
            {
                return accessTokenElement.GetString();
            }

            return null;
        }

        public static async Task<string?> GetOAuthAccessTokenAsync(string clientId, string clientSecret, string redirectUri, string[] scopes)
        {
            string state = Guid.NewGuid().ToString("N");
            string scopeString = string.Join("+", scopes);

            string authUrl = $"https://id.twitch.tv/oauth2/authorize" +
                             $"?client_id={clientId}" +
                             $"&redirect_uri={redirectUri}" +
                             $"&response_type=code" +
                             $"&scope={scopeString}" +
                             $"&state={state}";

            Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });

            var listener = new HttpListener();
            listener.Prefixes.Add(redirectUri);
            listener.Start();

            var context = await listener.GetContextAsync();
            string code = context.Request.QueryString["code"];
            string receivedState = context.Request.QueryString["state"];

            var responseString = "<html><body>✅ Авторизация прошла. Можешь закрыть окно.</body></html>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer);
            context.Response.Close();
            listener.Stop();

            if (string.IsNullOrEmpty(code) || receivedState != state)
            {
                throw new Exception("Invalid authorization");
            }

            // Обмениваем code на access_token
            using var client = new HttpClient();
            var tokenResponse = await client.PostAsync("https://id.twitch.tv/oauth2/token", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", redirectUri }
            }));

            var json = await tokenResponse.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            string accessToken = doc.RootElement.GetProperty("access_token").GetString();

            return accessToken;
        }
    }
}
