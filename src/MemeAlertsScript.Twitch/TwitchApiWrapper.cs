using System.Diagnostics;
using System.Net;
using System.Text.Json;
using TwitchLib.Api;

namespace MemeAlertsScript.Twitch
{
    public class TwitchApiWrapper
    {
        private readonly TwitchAPI _api;
        private readonly string _appId;
        private readonly string _appSecret;
        private readonly string _redirectUri;
        private readonly string[] _scopes;

        public TwitchApiWrapper(string appId, string appSecret, string appToken, string redirectUri, string[] scopes)
        {
            _api = new TwitchAPI();
            _api.Settings.ClientId = appId;
            _api.Settings.AccessToken = appToken;

            _appId = appId;
            _appSecret = appSecret;
            _redirectUri = redirectUri;
            _scopes = scopes;
        }

        public async Task<string?> GetOAuthTokenAsync()
        {
            string state = Guid.NewGuid().ToString("N");
            string scopeString = string.Join("+", _scopes);

            string authUrl = $"https://id.twitch.tv/oauth2/authorize" +
                             $"?client_id={_appId}" +
                             $"&redirect_uri={_redirectUri}" +
                             $"&response_type=code" +
                             $"&scope={scopeString}" +
                             $"&state={state}";

            Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });

            var listener = new HttpListener();
            listener.Prefixes.Add(_redirectUri);
            listener.Start();

            var context = await listener.GetContextAsync();
            string code = context.Request.QueryString["code"];
            string receivedState = context.Request.QueryString["state"];

            var responseString = "<html><body>Done. You can close this window.</body></html>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer);
            context.Response.Close();
            listener.Stop();

            if (string.IsNullOrEmpty(code) || receivedState != state)
            {
                throw new Exception("Invalid authorization");
            }

            using var client = new HttpClient();
            var tokenResponse = await client.PostAsync("https://id.twitch.tv/oauth2/token", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", _appId },
                { "client_secret", _appSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _redirectUri }
            }));

            var json = await tokenResponse.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(json);
            string accessToken = document.RootElement.GetProperty("access_token").GetString();

            return accessToken;
        }

        public async Task<string?> GetUserIdByLoginAsync(string login)
        {
            var users = await _api.Helix.Users.GetUsersAsync(logins: new List<string> { login });
            return users.Users.FirstOrDefault()?.Id;
        }

    }
}
