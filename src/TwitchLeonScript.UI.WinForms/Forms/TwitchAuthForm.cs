using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.Web.WebView2.Core;
using System.Web;
using TwitchLeonScript.Core.App.Commands.TwitchAuth;
using TwitchLeonScript.Core.Common.Options;
using TwitchLeonScript.UI.Tokens.Infrastructure;
using TwitchLeonScript.UI.Tokens.StoredTokens;

namespace TwitchLeonScript.UI.WinForms.Forms
{
    internal partial class TwitchAuthForm : Form
    {
        private readonly TwitchOptions _options;
        private readonly IMediator _mediator;
        private readonly ITwitchTokenStorage _tokenStorage;

        public TwitchAuthForm(IOptions<TwitchOptions> options, IMediator mediator, ITwitchTokenStorage tokenStorage)
        {
            InitializeComponent();
            this._options = options.Value;
            this._mediator = mediator;
            this._tokenStorage = tokenStorage;
        }

        private async void OAuthForm_Load(object sender, EventArgs e)
        {
            var command = new GetTwitchStateCommand();
            var response = await this._mediator.Send(command);

            await this.wv2TwitchAuth.EnsureCoreWebView2Async();
            this.wv2TwitchAuth.Source = response.Uri;

            this.wv2TwitchAuth.CoreWebView2.NavigationStarting += async (s, args) =>
            {
                if (args.Uri.StartsWith(this._options.RedirectUri) && args.Uri.Contains("code="))
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
                    var accessTokenResponse = await this._mediator.Send(accessTokenCommand);

                    var oauthTokenCommand = new GetTwitchOAuthCommand { Code = code };
                    var oauthTokenResponse = await this._mediator.Send(oauthTokenCommand);


                    var broadcasterCommand = new GetTwitchBroadcasterCommand { OAuthToken = oauthTokenResponse.OAuthToken.AccessToken };
                    var broadcasterResponse = await this._mediator.Send(broadcasterCommand);

                    this._tokenStorage.Save(new StoredTwitchToken
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
