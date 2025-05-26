using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.Web.WebView2.Core;
using System.Web;
using TwitchLeonScript.Core.App.Options;
using TwitchLeonScript.Core.App.Queries.TwitchAppToken;
using TwitchLeonScript.Core.App.Queries.TwitchBroadcaster;
using TwitchLeonScript.Core.App.Queries.TwitchOAuth;
using TwitchLeonScript.Core.App.Queries.TwitchState;
using TwitchLeonScript.UI.WinForms.Tokens.Infrastructure;
using TwitchLeonScript.UI.WinForms.Tokens.StorageModels;

namespace TwitchLeonScript.UI.WinForms.Forms
{
    internal partial class TwitchAuthForm : Form
    {
        private readonly TwitchOptions _options;
        private readonly IMediator _mediator;
        private readonly ITwitchTokenStorage _tokenStorage;

        public TwitchAuthForm(IOptions<TwitchOptions> options, IMediator mediator, ITwitchTokenStorage tokenStorage)
        {
            _options = options.Value;
            _mediator = mediator;
            _tokenStorage = tokenStorage;

            InitializeComponent();
        }

        private async void OAuthForm_Load(object sender, EventArgs e)
        {
            var command = new GetTwitchStateCommand();
            var response = await _mediator.Send(command);

            await wv2TwitchAuth.EnsureCoreWebView2Async();
            wv2TwitchAuth.Source = response.Uri;

            wv2TwitchAuth.CoreWebView2.NavigationStarting += async (s, args) =>
            {
                if (args.Uri.StartsWith(_options.RedirectUri) && args.Uri.Contains("code="))
                {
                    var uri = new Uri(args.Uri);
                    var query = HttpUtility.ParseQueryString(uri.Query);
                    var code = query["code"]!;
                    var receivedState = query["state"]!;

                    if (receivedState != response.State)
                    {
                        DialogResult = DialogResult.Cancel;
                        Close();
                        return;
                    }

                    var accessTokenCommand = new GetTwitchAppTokenCommand();
                    var accessTokenResponse = await _mediator.Send(accessTokenCommand);

                    if (accessTokenResponse is null || string.IsNullOrEmpty(accessTokenResponse.AccessToken))
                    {
                        MessageBox.Show("Failed to retrieve access token information. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.Abort;
                        Close();
                        return;
                    }

                    var oauthTokenCommand = new GetTwitchOAuthCommand { Code = code };
                    var oauthTokenResponse = await _mediator.Send(oauthTokenCommand);

                    if (oauthTokenResponse is null || oauthTokenResponse.OAuthToken is null || string.IsNullOrEmpty(oauthTokenResponse.OAuthToken.AccessToken))
                    {
                        MessageBox.Show("Failed to retrieve OAuth token information. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.Abort;
                        Close();
                        return;
                    }

                    var broadcasterCommand = new GetTwitchBroadcasterCommand { OAuthToken = oauthTokenResponse.OAuthToken.AccessToken };
                    var broadcasterResponse = await _mediator.Send(broadcasterCommand);

                    if (broadcasterResponse is null || broadcasterResponse.Broadcaster is null)
                    {
                        MessageBox.Show("Failed to retrieve broadcaster information. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.Abort;
                        Close();
                        return;
                    }

                    _tokenStorage.Save(new StoredTwitchToken
                    {
                        AccessToken = accessTokenResponse.AccessToken,
                        OAuthToken = oauthTokenResponse.OAuthToken.AccessToken,
                        RefreshToken = oauthTokenResponse.OAuthToken.RefreshToken,
                        BroadcasterId = broadcasterResponse.Broadcaster.Id,
                        BroadcasterName = broadcasterResponse.Broadcaster.Login,
                        ExpiresAt = oauthTokenResponse.OAuthToken.ExpiresAt
                    });

                    DialogResult = DialogResult.OK;
                    Close();
                }
            };
        }

        private async void OAuthForm_FormClosed(object sender, EventArgs e)
        {
            await wv2TwitchAuth.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
        }
    }
}
