using MediatR;
using MemeAlertsScript.Core.App.Commands.TwitchAuth;
using MemeAlertsScript.Core.Common.Options;
using Microsoft.Extensions.Options;
using Microsoft.Web.WebView2.Core;
using System.Web;
using TwitchLeonScript.WinForms.Tokens.Infrastructure;
using TwitchLeonScript.WinForms.Tokens.StoredTokens;

namespace MemeAlertsScript.WinForms
{
    internal partial class TwitchAuthForm : Form
    {
        private readonly TwitchOptions options;
        private readonly IMediator mediator;
        private readonly ITwitchTokenStorage tokenStorage;

        public TwitchAuthForm(IOptions<TwitchOptions> options, IMediator mediator, ITwitchTokenStorage tokenStorage)
        {
            InitializeComponent();
            this.options = options.Value;
            this.mediator = mediator;
            this.tokenStorage = tokenStorage;
        }

        private async void OAuthForm_Load(object sender, EventArgs e)
        {
            var command = new GetTwitchStateCommand();
            var response = await this.mediator.Send(command);

            await this.wv2TwitchAuth.EnsureCoreWebView2Async();
            this.wv2TwitchAuth.Source = response.Uri;

            this.wv2TwitchAuth.CoreWebView2.NavigationStarting += async (s, args) =>
            {
                if (args.Uri.StartsWith(this.options.RedirectUri) && args.Uri.Contains("code="))
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
                    var accessTokenResponse = await this.mediator.Send(accessTokenCommand);

                    if (accessTokenResponse.AccessToken == null)
                    {
                        DialogResult = DialogResult.Cancel;
                        Close();
                        return;
                    }

                    var oauthTokenCommand = new GetTwitchOAuthCommand { Code = code };
                    var oauthTokenResponse = await this.mediator.Send(oauthTokenCommand);

                    if (oauthTokenResponse.OAuthToken == null)
                    {
                        DialogResult = DialogResult.Cancel;
                        Close();
                        return;
                    }

                    var broadcasterCommand = new GetTwitchBroadcasterCommand { OAuthToken = oauthTokenResponse.OAuthToken.AccessToken };
                    var broadcasterResponse = await this.mediator.Send(broadcasterCommand);

                    if (broadcasterResponse == null)
                    {
                        DialogResult = DialogResult.Abort;
                        Close();
                        return;
                    }

                    this.tokenStorage.Save(new StoredTwitchToken
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
            await this.wv2TwitchAuth.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
        }
    }
}
