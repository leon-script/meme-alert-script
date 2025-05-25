using MediatR;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using TwitchLeonScript.Core.App.Queries.MemeAuth;
using TwitchLeonScript.Core.Meme.Models;
using TwitchLeonScript.UI.Common.Helpers;
using TwitchLeonScript.UI.Tokens.Infrastructure;
using TwitchLeonScript.UI.Tokens.StoredTokens;

namespace TwitchLeonScript.UI.WinForms.Forms
{
    internal partial class MemeAuthForm : Form
    {
        private readonly IMediator _mediator;
        private readonly IMemeTokenStorage _tokenStorage;

        public MemeAuthForm(IMediator mediator, IMemeTokenStorage tokenStorage)
        {
            _mediator = mediator;
            _tokenStorage = tokenStorage;

            InitializeComponent();
        }

        private async void LoginMemeForm_Load(object sender, EventArgs e)
        {
            await wv2MemeAuth.EnsureCoreWebView2Async();
            wv2MemeAuth.Source = new Uri("https://memealerts.com/");

            wv2MemeAuth.CoreWebView2.NavigationCompleted += async (_, args) =>
            {
                while (wv2MemeAuth.CoreWebView2 != null)
                {
                    var javaScript = "JSON.stringify(localStorage)";
                    var raw = await wv2MemeAuth.CoreWebView2.ExecuteScriptAsync(javaScript);

                    if (CsIaHelper.IsCsIaTrue(raw))
                    {
                        var unescaped = JsonSerializer.Deserialize<string>(raw);

                        if (string.IsNullOrEmpty(unescaped))
                        {
                            await Task.Delay(100);
                            continue;
                        }

                        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(unescaped);

                        if (dictionary != null &&
                            dictionary.TryGetValue("accessToken", out var accessToken) &&
                            dictionary.TryGetValue("refreshToken", out var refreshToken))
                        {
                            var oauthToken = new MemeOAuthTokenDto
                            {
                                AccessToken = accessToken,
                                RefreshToken = refreshToken
                            };

                            var command = new GetMemeBroadcasterCommand { OAuthToken = oauthToken.AccessToken };
                            var broadcasterResponse = await _mediator.Send(command);

                            if (broadcasterResponse.Broadcaster == null)
                            {
                                MessageBox.Show("Failed to retrieve broadcaster information. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                DialogResult = DialogResult.Abort;
                                Close();
                                return;
                            }

                            _tokenStorage.Save(new StoredMemeToken
                            {
                                OAuthToken = accessToken,
                                RefreshToken = refreshToken,
                                BroadcasterId = broadcasterResponse.Broadcaster.Id,
                                BroadcasterName = broadcasterResponse.Broadcaster.Name,
                                ExpiresAt = DateTime.UtcNow.AddMinutes(60),
                            });

                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        else
                        {
                            DialogResult = DialogResult.Abort;
                            Close();
                            return;
                        }
                    }

                    await Task.Delay(100);
                }
            };
        }

        private async void LoginMemeForm_FormClosing(object sender, EventArgs e)
        {
            await wv2MemeAuth.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
        }
    }
}
