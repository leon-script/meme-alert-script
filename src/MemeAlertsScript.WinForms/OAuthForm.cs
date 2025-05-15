using MemeAlertsScript.Core.Models;
using MemeAlertsScript.Twitch;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;

namespace MemeAlertsScript.WinForms
{
    public partial class OAuthForm : Form
    {
        private readonly TwitchSettings _config;

        public TwitchBroadcaster? BroadcasterResult { get; private set; }
        public TwitchAuthTokens? AuthTokensResult { get; private set; }

        public OAuthForm(TwitchSettings config)
        {
            _config = config;
            InitializeComponent();
        }

        private async void OAuthForm_Load(object sender, EventArgs e)
        {
            await twitchWebView2.EnsureCoreWebView2Async();

            var state = TwitchOAuthHelper.CreateOAuthState();
            twitchWebView2.Source = TwitchOAuthHelper.CreateOAuthUrl(_config.AppId, _config.RedirectUri, _config.Scopes, state);

            twitchWebView2.CoreWebView2.NavigationStarting += async (s, args) =>
            {
                if (args.Uri.StartsWith(_config.RedirectUri) && args.Uri.Contains("code="))
                {
                    var uri = new Uri(args.Uri);
                    var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
                    string code = query["code"]!;
                    string receivedState = query["state"]!;

                    if (receivedState != state)
                    {
                        MessageBox.Show("State mismatch");
                        DialogResult = DialogResult.Cancel;
                        Close();
                        return;
                    }

                    var authTokens = await ExchangeCodeForTokenAsync(code);
                    if (authTokens == null || authTokens.AccessToken == null)
                    {
                        DialogResult = DialogResult.Abort;
                        Close();
                        return;
                    }

                    var broadcaster = await TwitchOAuthHelper.GetTwitchUserInfoAsync(_config.AppId, authTokens.AccessToken);
                    if (broadcaster == null)
                    {
                        DialogResult = DialogResult.Abort;
                        Close();
                        return;
                    }

                    AuthTokensResult = authTokens;
                    BroadcasterResult = broadcaster;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
        }

        private async Task<TwitchAuthTokens?> ExchangeCodeForTokenAsync(string code)
        {
            using var client = new HttpClient();

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", _config.AppId },
                { "client_secret", _config.AppSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", _config.RedirectUri }
            });

            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed to get token");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(json);

            return new TwitchAuthTokens()
            {
                AccessToken = document.RootElement.GetProperty("access_token").GetString()!,
                RefreshToken = document.RootElement.GetProperty("refresh_token").GetString()!,
                ExpiresAt = DateTime.UtcNow.AddSeconds(document.RootElement.GetProperty("expires_in").GetInt32()),
                Scope = document.RootElement.GetProperty("scope").EnumerateArray().Select(x => x.GetString()).ToArray()!,
                TokenType = document.RootElement.GetProperty("token_type").GetString()!
            };
        }

        private async void OAuthForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            await twitchWebView2.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
        }
    }
}
