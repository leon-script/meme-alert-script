using MemeAlertsScript.Core.Extensions;
using MemeAlertsScript.Core.Models;
using MemeAlertsScript.Twitch;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using System.Web;

namespace MemeAlertsScript.WinForms
{
    public partial class OAuthForm : Form
    {
        private readonly string _appId;
        private readonly string _appSecret;
        private readonly string _redirectUri;
        private readonly string[] _scopes;
        private readonly ILogger _logger;

        public TwitchBroadcaster? BroadcasterResult { get; private set; }
        public TwitchAuthTokens? AuthTokensResult { get; private set; }

        public OAuthForm(string appId, string appSecret, string redirectUri, string[] scopes, ILogger logger)
        {
            InitializeComponent();
            _appId = appId;
            _appSecret = appSecret;
            _redirectUri = redirectUri;
            _scopes = scopes;
            _logger = logger;
        }

        private async void OAuthForm_Load(object sender, EventArgs e)
        {
            _logger.LogDebug("Initializing WebView2...");
            await twitchWebView2.EnsureCoreWebView2Async();

            var state = TwitchOAuthHelper.CreateOAuthState();
            var authUri = TwitchOAuthHelper.CreateOAuthUrl(_appId, _redirectUri, _scopes, state);

            _logger.LogDebug("Navigating to Twitch OAuth URL.");
            twitchWebView2.Source = authUri;

            twitchWebView2.CoreWebView2.NavigationStarting += async (s, args) =>
            {
                _logger.LogDebug("Navigation starting: {Url}", args.Uri);

                if (args.Uri.StartsWith(_redirectUri) && args.Uri.Contains("code="))
                {
                    _logger.LogDebug("Authorization redirect detected.");

                    var uri = new Uri(args.Uri);
                    var query = HttpUtility.ParseQueryString(uri.Query);
                    string code = query["code"]!;
                    string receivedState = query["state"]!;

                    if (receivedState != state)
                    {
                        _logger.LogError("State mismatch: expected {Expected}, received {Actual}", state, receivedState);
                        DialogResult = DialogResult.Cancel;
                        Close();
                        return;
                    }

                    _logger.LogDebug("State validated. Exchanging code for token...");
                    var authTokens = await ExchangeCodeForTokenAsync(code);

                    if (authTokens == null || authTokens.AccessToken == null)
                    {
                        _logger.LogError("Failed to retrieve access token.");
                        DialogResult = DialogResult.Abort;
                        Close();
                        return;
                    }

                    _logger.LogDebug("Token exchange successful. Access token: {TokenPreview}", authTokens.AccessToken.ToSecretPreview());

                    _logger.LogDebug("Retrieving Twitch user info...");
                    var broadcaster = await TwitchOAuthHelper.GetTwitchUserInfoAsync(_appId, authTokens.AccessToken);

                    if (broadcaster == null)
                    {
                        _logger.LogError("Failed to retrieve Twitch user info.");
                        DialogResult = DialogResult.Abort;
                        Close();
                        return;
                    }

                    _logger.LogInformation("Twitch user identified: {Login} (ID: {Id})", broadcaster.Login, broadcaster.Id);
                    AuthTokensResult = authTokens;
                    BroadcasterResult = broadcaster;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
        }

        private async Task<TwitchAuthTokens?> ExchangeCodeForTokenAsync(string code)
        {
            try
            {
                using var client = new HttpClient();

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", _appId },
                    { "client_secret", _appSecret },
                    { "code", code },
                    { "grant_type", "authorization_code" },
                    { "redirect_uri", _redirectUri }
                });

                var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Token request failed with status code {StatusCode}.", response.StatusCode);
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var document = JsonDocument.Parse(json);

                _logger.LogDebug("Token response received: {Json}", json);

                return new TwitchAuthTokens
                {
                    AccessToken = document.RootElement.GetProperty("access_token").GetString()!,
                    RefreshToken = document.RootElement.GetProperty("refresh_token").GetString()!,
                    ExpiresAt = DateTime.UtcNow.AddSeconds(document.RootElement.GetProperty("expires_in").GetInt32()),
                    Scope = document.RootElement.GetProperty("scope").EnumerateArray().Select(x => x.GetString()!).ToArray(),
                    TokenType = document.RootElement.GetProperty("token_type").GetString()!
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred while exchanging code for token.");
                return null;
            }
        }

        private async void OAuthForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _logger.LogDebug("OAuth form closed. Clearing WebView2 profile data...");

            try
            {
                await twitchWebView2.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
                _logger.LogDebug("WebView2 profile data cleared.");
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "Failed to clear WebView2 browsing data.");
            }
        }
    }
}
