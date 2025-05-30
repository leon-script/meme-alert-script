using MediatR;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using TwitchLeonScript.Application.Commands.LoginMeme;
using TwitchLeonScript.Application.Commands.SecondLoginMeme;
using TwitchLeonScript.Application.Queries.MemeState;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Session.Models;
using TwitchLeonScript.Session.Storages;

namespace TwitchLeonScript.WinForms.Forms
{
    internal partial class MemeAuthForm : Form
    {
        private readonly IMediator _mediator;
        private readonly MemeSessionStorage _tokenStorage;

        public MemeAuthForm(IMediator mediator, MemeSessionStorage tokenStorage)
        {
            _mediator = mediator;
            _tokenStorage = tokenStorage;

            InitializeComponent();
        }

        private async void LoginMemeForm_Load(object sender, EventArgs e)
        {
            var token = _tokenStorage.Load();
            if (token is not null && DateTime.UtcNow <= token.OAuthExpiresAt.Subtract(TimeSpan.FromMinutes(1)))
            {
                await RefreshToken(token);
                Close();
                return;
            }

            await wv2MemeAuth.EnsureCoreWebView2Async();
            ConfigureWebViewNavigation();
            wv2MemeAuth.Source = new Uri("https://memealerts.com/");
        }

        private void ConfigureWebViewNavigation()
        {
            wv2MemeAuth.CoreWebView2.NavigationStarting += async (_, args) =>
            {
                try
                {
                    while (wv2MemeAuth.CoreWebView2 is not null)
                    {
                        var json = await wv2MemeAuth.CoreWebView2.ExecuteScriptAsync("JSON.stringify(localStorage)");
                        var stateResponse = await _mediator.Send(new GetMemeStateQuery { Json = json });

                        if (!stateResponse.IsSuccess || stateResponse.Value is null)
                        {
                            ShowError("Failed to retrieve state information.", stateResponse.Error);
                            CloseForm(DialogResult.Abort);
                            return;
                        }

                        if (!stateResponse.Value.IsCsIaAuth)
                        {
                            continue;
                        }

                        var unescaped = JsonSerializer.Deserialize<string>(json);
                        if (unescaped is null)
                        {
                            await Task.Delay(100);
                            continue;
                        }

                        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(unescaped);
                        if (dictionary is null ||
                            !dictionary.TryGetValue("accessToken", out var accessToken) ||
                            !dictionary.TryGetValue("refreshToken", out var refreshToken))
                        {
                            ShowError("Failed to retrieve access token or refresh token.");
                            CloseForm(DialogResult.Abort);
                            return;
                        }

                        var loginResponse = await _mediator.Send(new LoginMemeCommand
                        {
                            OAuthToken = accessToken,
                            RefreshToken = refreshToken
                        });

                        if (!loginResponse.IsSuccess || loginResponse.Value is null)
                        {
                            ShowError("Failed to auth.", loginResponse.Error);
                            CloseForm(DialogResult.Abort);
                            return;
                        }

                        SaveSession(loginResponse.Value);
                        CloseForm(DialogResult.OK);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ShowError("Unexpected error during login process.", ex.Message);
                    CloseForm(DialogResult.Abort);
                }
            };
        }

        private async Task RefreshToken(MemeSession token)
        {
            var refreshResponse = await _mediator.Send(new SecondLoginMemeCommand
            {
                OAuthToken = new MemeOAuthToken
                {
                    Token = token.OAuthToken,
                    RefreshToken = token.OAuthRefreshToken,
                    ExpiresAt = token.OAuthExpiresAt
                },
                Broadcaster = new MemeBroadcaster
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
            }

            DialogResult = DialogResult.OK;
        }

        private void SaveSession(LoginMemeResponse response)
        {
            _tokenStorage.Save(new MemeSession
            {
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

        private async void LoginMemeForm_FormClosing(object sender, EventArgs e)
        {
            if (wv2MemeAuth?.CoreWebView2?.Profile != null)
            {
                await wv2MemeAuth.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
            }
        }
    }
}
