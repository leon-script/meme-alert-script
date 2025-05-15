using MemeAlertsScript.Core.Extensions;
using MemeAlertsScript.Core.Models;
using MemeAlertsScript.Twitch;
using MemeAlertsScript.WinForms.Configs;
using MemeAlertsScript.WinForms.Logging;
using Microsoft.Extensions.Logging;

namespace MemeAlertsScript.WinForms
{
    public partial class MainForm : Form
    {
        private readonly Configuration.AppSettings _config;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private EventSubListener? _eventSub;
        private TwitchAuthTokens? _authTokens;
        private TwitchBroadcaster? _broadcaster;

        public MainForm()
        {
            InitializeComponent();

            _config = Configuration.LoadAppSettings();
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(_config.Logging.LogLevel.ToLogLevelOrDefault());
                builder.AddProvider(new RichTextBoxLoggerProvider(this.logsRichTextBox));
            });

            _logger = _loggerFactory.CreateLogger("Main");
            _logger.LogDebug("MainForm initialized.");
        }

        private void MainForm_LoadAsync(object sender, EventArgs e)
        {
            _logger.LogDebug("MainForm loaded.");
        }

        private async void MainForm_FormClosing(object sender, EventArgs e)
        {
            _logger.LogDebug("Application is closing...");

            try
            {
                if (_eventSub != null)
                {
                    _logger.LogDebug("Stopping EventSub listener...");
                    _eventSub.OnRewardRedeemed -= OnRewardRedeemed;
                    await _eventSub.StopAsync(CancellationToken.None);
                    _logger.LogDebug("EventSub listener stopped.");
                }

                _logger.LogDebug("Shutdown complete.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred during closing.");
            }
        }

        private async void buttonTwitchLogin_Click(object sender, EventArgs e)
        {
            _logger.LogInformation("Twitch login started.");

            try
            {
                using var oauthForm = new OAuthForm(
                    _config.Twitch.AppId,
                    _config.Twitch.AppSecret,
                    _config.Twitch.RedirectUri,
                    _config.Twitch.Scopes,
                    _loggerFactory.CreateLogger("Auth"));

                var dialogResult = oauthForm.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    _authTokens = oauthForm.AuthTokensResult;
                    _broadcaster = oauthForm.BroadcasterResult;
                    textBoxTwitchLogin.Text = $"{_broadcaster!.Login} (ID: {_broadcaster.Id})";
                    buttonTwitchLogin.Enabled = false;
                    buttonTwitchLogout.Enabled = true;

                    _logger.LogInformation("Twitch OAuth login successful.");
                    _logger.LogInformation("Logged in as: {Login} (ID: {Id})", _broadcaster.Login, _broadcaster.Id);
                    _logger.LogDebug("Access token: {Token}", _authTokens!.AccessToken.ToSecretPreview());

                    _logger.LogInformation("Requesting application access token...");
                    var appToken = await TwitchTokenHelper.GetAppTokenAsync(_config.Twitch.AppId, _config.Twitch.AppSecret);
                    _logger.LogDebug("App token received: {AppToken}", appToken.ToSecretPreview());

                    _eventSub = new EventSubListener(_config.Twitch.AppId, appToken!, _broadcaster.Id!, _authTokens.AccessToken!);
                    _eventSub.OnRewardRedeemed += OnRewardRedeemed;

                    _logger.LogInformation("Starting EventSub listener...");
                    await _eventSub.StartAsync(CancellationToken.None);
                    _logger.LogInformation("EventSub listener started.");
                }
                else
                {
                    _logger.LogWarning("Twitch OAuth login cancelled by user.");

                    _authTokens = null;
                    _broadcaster = null;
                    textBoxTwitchLogin.Text = string.Empty;
                    buttonTwitchLogin.Enabled = true;
                    buttonTwitchLogout.Enabled = false;

                    if (_eventSub != null)
                    {
                        _logger.LogInformation("Stopping EventSub listener (cleanup)...");
                        _eventSub.OnRewardRedeemed -= OnRewardRedeemed;
                        await _eventSub.StopAsync(CancellationToken.None);
                        _logger.LogInformation("EventSub listener stopped.");
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred during authorization.");
            }
        }

        private async void buttonTwitchLogout_Click(object sender, EventArgs e)
        {
            _logger.LogInformation("Twitch logout requested.");

            try
            {
                _authTokens = null;
                _broadcaster = null;
                textBoxTwitchLogin.Text = string.Empty;
                buttonTwitchLogin.Enabled = true;
                buttonTwitchLogout.Enabled = false;

                if (_eventSub != null)
                {
                    _logger.LogInformation("Stopping EventSub listener...");
                    _eventSub.OnRewardRedeemed -= OnRewardRedeemed;
                    await _eventSub.StopAsync(CancellationToken.None);
                    _logger.LogInformation("EventSub listener stopped.");
                }

                _logger.LogInformation("Twitch OAuth logout completed.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred during logout.");
            }
        }

        private void OnRewardRedeemed(string userName, string rewardTitle)
        {
            _logger.LogInformation("Reward redeemed: {User} - {Reward}", userName, rewardTitle);
        }
    }
}
