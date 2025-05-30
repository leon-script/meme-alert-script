using MediatR;
using Microsoft.Web.WebView2.Core;
using System.Web;
using TwitchLeonScript.Application.Commands.LoginTwitch;
using TwitchLeonScript.Application.Commands.SecondLoginTwitch;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Application.Queries.TwitchState;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Session.Models;
using TwitchLeonScript.Session.Storages;

namespace TwitchLeonScript.WinForms.Forms
{
    internal partial class TwitchAuthForm : Form
    {
        private readonly IMediator _mediator;
        private readonly TwitchSessionStorage _tokenStorage;

        public TwitchAuthForm(IMediator mediator, TwitchSessionStorage tokenStorage)
        {
            _mediator = mediator;
            _tokenStorage = tokenStorage;

            InitializeComponent();
        }

        private async void OAuthForm_Load(object sender, EventArgs e)
        {
            var token = _tokenStorage.Load();
            if (token is not null && DateTime.UtcNow <= token.OAuthExpiresAt.Subtract(TimeSpan.FromMinutes(1)))
            {
                await RefreshToken(token);
                Close();
                return;
            }

            var stateResponse = await _mediator.Send(new GetTwitchStateQuery());
            if (!stateResponse.IsSuccess || stateResponse.Value is null)
            {
                ShowError("Failed to retrieve state information.", stateResponse.Error);
                CloseForm(DialogResult.Abort);
                return;
            }

            await wv2TwitchAuth.EnsureCoreWebView2Async();
            ConfigureWebViewNavigation(stateResponse.Value);
            wv2TwitchAuth.Source = stateResponse.Value.OAuthUri;
        }

        private void ConfigureWebViewNavigation(GetTwitchStateResponse state)
        {
            wv2TwitchAuth.CoreWebView2.NavigationStarting += async (_, args) =>
            {
                if (!args.Uri.StartsWith(state.RedirectUri) || !args.Uri.Contains("code="))
                {
                    return;
                }

                try
                {
                    var uri = new Uri(args.Uri);
                    var query = HttpUtility.ParseQueryString(uri.Query);

                    var code = query["code"];
                    var receivedState = query["state"];

                    if (receivedState != state.State)
                    {
                        ShowError("State mismatch.");
                        CloseForm(DialogResult.Abort);
                        return;
                    }

                    var response = await _mediator.Send(new LoginTwitchCommand { Code = code });
                    if (!response.IsSuccess || response.Value is null)
                    {
                        ShowError("Failed to auth.", response.Error);
                        CloseForm(DialogResult.Abort);
                        return;
                    }

                    SaveSession(response.Value);
                    CloseForm(DialogResult.OK);
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error during login process.", ex.Message);
                    CloseForm(DialogResult.Abort);
                }
            };
        }

        private async Task RefreshToken(TwitchSession token)
        {
            var refreshResponse = await _mediator.Send(new SecondLoginTwitchCommand
            {
                AppToken = new TwitchAppToken
                {
                    Token = token.AppToken
                },
                OAuthToken = new TwitchOAuthToken
                {
                    Token = token.OAuthToken,
                    RefreshToken = token.OAuthRefreshToken,
                    ExpiresAt = token.OAuthExpiresAt
                },
                Broadcaster = new TwitchBroadcaster
                {
                    Id = token.BroadcasterId,
                    Login = token.BroadcasterLogin
                }
            });

            if (!refreshResponse.IsSuccess || refreshResponse.Value is null)
            {
                _tokenStorage.Clear();
                MessageBox.Show($"Failed to refresh session. Please re-authenticate. Please try again.\n\n{refreshResponse.Error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void SaveSession(LoginTwitchResponse response)
        {
            _tokenStorage.Save(new TwitchSession
            {
                AppToken = response.AppToken.Token,
                OAuthToken = response.OAuthToken.Token,
                OAuthRefreshToken = response.OAuthToken.RefreshToken,
                OAuthExpiresAt = response.OAuthToken.ExpiresAt,
                BroadcasterId = response.Broadcaster.Id,
                BroadcasterLogin = response.Broadcaster.Login,
            });
        }

        private static void ShowError(string message, string? errorMessage = null)
        {
            MessageBox.Show($"{message} Please try again.\n\n{errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CloseForm(DialogResult result)
        {
            //BeginInvoke(() =>
            //{
            //    DialogResult = result;
            //    Close();
            //});
            DialogResult = result;
            Close();
        }

        private async void OAuthForm_FormClosed(object sender, EventArgs e)
        {
            if (wv2TwitchAuth?.CoreWebView2?.Profile != null)
            {
                await wv2TwitchAuth.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
            }
        }
    }
}
