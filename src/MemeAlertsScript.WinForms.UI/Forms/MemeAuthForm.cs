using MediatR;
using MemeAlertsScript.Core.App.Commands.MemeAuth;
using MemeAlertsScript.Core.Meme.Models;
using Microsoft.Web.WebView2.Core;
using System.Text.Json;
using TwitchLeonScript.WinForms.Tokens.Infrastructure;
using TwitchLeonScript.WinForms.Tokens.StoredTokens;

namespace MemeAlertsScript.WinForms
{
    internal partial class MemeAuthForm : Form
    {
        private readonly IMediator mediator;
        private readonly IMemeTokenStorage tokenStorage;

        public MemeAuthForm(IMediator mediator, IMemeTokenStorage tokenStorage)
        {
            InitializeComponent();
            this.mediator = mediator;
            this.tokenStorage = tokenStorage;
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
                            var broadcasterResponse = await this.mediator.Send(command);

                            if (broadcasterResponse == null)
                            {
                                DialogResult = DialogResult.Abort;
                                Close();
                                return;
                            }

                            this.tokenStorage.Save(new StoredMemeToken
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
