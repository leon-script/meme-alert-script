using MediatR;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using TwitchLeonScript.Core.App.Commands.MemeAuth;
using TwitchLeonScript.Core.Meme.Models;
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
            InitializeComponent();
            this._mediator = mediator;
            this._tokenStorage = tokenStorage;
        }

        private async void LoginMemeForm_Load(object sender, EventArgs e)
        {
            await this.wv2MemeAuth.EnsureCoreWebView2Async();
            this.wv2MemeAuth.Source = new Uri("https://memealerts.com/");

            this.wv2MemeAuth.CoreWebView2.NavigationCompleted += async (_, args) =>
            {
                while (this.wv2MemeAuth.CoreWebView2 != null)
                {
                    var javaScript = "JSON.stringify(localStorage)";
                    var raw = await this.wv2MemeAuth.CoreWebView2.ExecuteScriptAsync(javaScript);

                    if (IsCsIaTrue(raw))
                    {
                        var unescaped = JsonSerializer.Deserialize<string>(raw);
                        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(unescaped!);

                        if (dictionary != null &&
                            dictionary.TryGetValue("accessToken", out var accessToken) &&
                            dictionary.TryGetValue("refreshToken", out var refreshToken))
                        {
                            var oauthToken = new MemeOAuthToken
                            {
                                AccessToken = accessToken,
                                RefreshToken = refreshToken
                            };

                            var command = new GetMemeBroadcasterCommand { OAuthToken = oauthToken.AccessToken };
                            var broadcasterResponse = await this._mediator.Send(command);

                            this._tokenStorage.Save(new StoredMemeToken
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
            await this.wv2MemeAuth.CoreWebView2.Profile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
        }

        private static bool IsCsIaTrue(string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
            {
                return false;
            }

            using var document = JsonDocument.Parse(rawInput);
            var root = document.RootElement;

            if (root.ValueKind == JsonValueKind.Object)
            {
                if (root.TryGetProperty("cs-ia", out var csIaProp))
                {
                    return csIaProp.GetString()?.ToLowerInvariant() == "true";
                }
            }

            if (root.ValueKind == JsonValueKind.String)
            {
                string innerJson = root.GetString()!;
                return IsCsIaTrue(innerJson);
            }

            return false;
        }
    }
}
