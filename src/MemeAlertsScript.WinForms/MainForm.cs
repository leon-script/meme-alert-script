using MemeAlertsScript.Core.Extensions;
using MemeAlertsScript.Core.Models;
using MemeAlertsScript.Twitch;
using MemeAlertsScript.WinForms.Configs;
using MemeAlertsScript.WinForms.Extensions;
using MemeAlertsScript.WinForms.Logging;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.Enums;
using TwitchLib.EventSub.Core.SubscriptionTypes.Channel;

namespace MemeAlertsScript.WinForms
{
    public partial class MainForm : Form
    {
        private readonly Configuration.AppSettings _config;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private TwitchEventSubListener? _eventSub;
        private TwitchRewardApi _rewardApi;
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
                    _eventSub.OnRewardRedeemed -= OnRewardRedeemedAsync;
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

                var dialogResult = oauthForm.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    _authTokens = oauthForm.AuthTokensResult;
                    _broadcaster = oauthForm.BroadcasterResult;
                    twitchLoginTextBox.Text = $"{_broadcaster!.Login} (ID: {_broadcaster.Id})";
                    twitchLoginButton.Enabled = false;
                    twitchLogoutButton.Enabled = true;

                    _logger.LogInformation("Twitch OAuth login successful.");
                    _logger.LogInformation("Logged in as: {Login} (ID: {Id})", _broadcaster.Login, _broadcaster.Id);
                    _logger.LogDebug("Access token: {Token}", _authTokens!.AccessToken.ToSecretPreview());

                    _logger.LogInformation("Requesting application access token...");
                    var appToken = await TwitchTokenHelper.GetAppTokenAsync(_config.Twitch.AppId, _config.Twitch.AppSecret);
                    _logger.LogDebug("App token received: {AppToken}", appToken.ToSecretPreview());

                    _eventSub = new TwitchEventSubListener(
                        _config.Twitch.AppId,
                        appToken!,
                        _broadcaster.Id!,
                        _authTokens.AccessToken!,
                        _loggerFactory.CreateLogger("EventSub"));

                    _rewardApi = new TwitchRewardApi(
                        _config.Twitch.AppId,
                        _authTokens.AccessToken!,
                        _broadcaster.Id!,
                        _loggerFactory.CreateLogger("RedemptionApi"));

                    _eventSub.OnRewardRedeemed += OnRewardRedeemedAsync;

                    _logger.LogInformation("Starting EventSub listener...");
                    await _eventSub.StartAsync(CancellationToken.None);
                    _logger.LogInformation("EventSub listener started.");

                    CleanRewards();
                    RefreshRewards();
                }
                else
                {
                    _logger.LogWarning("Twitch OAuth login cancelled by user.");

                    _authTokens = null;
                    _broadcaster = null;
                    twitchLoginTextBox.Text = string.Empty;
                    twitchLoginButton.Enabled = true;
                    twitchLogoutButton.Enabled = false;
                    CleanRewards();

                    if (_eventSub != null)
                    {
                        _logger.LogInformation("Stopping EventSub listener (cleanup)...");
                        _eventSub.OnRewardRedeemed -= OnRewardRedeemedAsync;
                        await _eventSub.StopAsync(CancellationToken.None);
                        _logger.LogInformation("EventSub listener stopped.");
                    }
                }
            }
            catch (Exception exception)
            {
                _authTokens = null;
                _broadcaster = null;
                twitchLoginTextBox.Text = string.Empty;
                twitchLoginButton.Enabled = true;
                twitchLogoutButton.Enabled = false;
                CleanRewards();

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
                twitchLoginTextBox.Text = string.Empty;
                twitchLoginButton.Enabled = true;
                twitchLogoutButton.Enabled = false;

                if (_eventSub != null)
                {
                    _logger.LogInformation("Stopping EventSub listener...");
                    _eventSub.OnRewardRedeemed -= OnRewardRedeemedAsync;
                    await _eventSub.StopAsync(CancellationToken.None);
                    _logger.LogInformation("EventSub listener stopped.");
                }

                _logger.LogInformation("Twitch OAuth logout completed.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred during logout.");
            }
            finally
            {
                CleanRewards();
            }
        }

        private async void OnRewardRedeemedAsync(ChannelPointsCustomRewardRedemption reward)
        {
            _logger.LogInformation($"Reward redeemed: {reward.UserName} - {reward.Reward.Title}");

            await _rewardApi.UpdateSingleRedemptionStatusAsync(
                rewardId: reward.Reward.Id,
                redemptionId: reward.Id,
                status: CustomRewardRedemptionStatus.FULFILLED
            );
        }

        private void buttonRefreshRewards_Click(object sender, EventArgs e)
        {
            CleanRewards();
            RefreshRewards();
        }

        private async void buttonCreateReward_Click(object sender, EventArgs e)
        {
            var createRewardForm = new CreateRewardForm();
            var dialogResult = createRewardForm.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                await _rewardApi.CreateCustomRewardAsync(
                    createRewardForm.RewardResult?.Title!,
                    createRewardForm.RewardResult!.TwitchCost,
                    createRewardForm.RewardResult?.Prompt!,
                    true,
                    true);

                CleanRewards();
                RefreshRewards();
            }
        }

        private void CleanRewards()
        {
            rewardsDataGridView.Rows.Clear();
        }

        private async void RefreshRewards()
        {
            var rewards = await _rewardApi.GetCustomRewardsAsync(_broadcaster?.Id!, true);

            if (rewards != null && rewards.Count > 0)
            {
                foreach (var reward in rewards)
                {
                    rewardsDataGridView.Rows.Add(
                        new object[]
                        {
                            reward.Id,
                            "Delete",
                            reward.Title,
                            reward.Cost,
                            reward.Prompt.ParseTrailingBracketNumber()!,
                            reward.Prompt
                        });
                }
            }
            else
            {
                _logger.LogWarning("No rewards found or unable to retrieve rewards.");
            }
        }

        private async void dataGridViewRewards_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                var grid = sender as DataGridView;
                var rewardId = grid?.Rows[e.RowIndex].Cells[0].Value?.ToString();

                if (!string.IsNullOrEmpty(rewardId))
                {
                    var confirm = MessageBox.Show(
                        "Are you sure you want to delete this reward?",
                        "Confirm Deletion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            await _rewardApi.DeleteCustomRewardAsync(rewardId);
                            grid!.Rows.RemoveAt(e.RowIndex);

                            _logger.LogInformation("Reward {RewardId} deleted successfully.", rewardId);
                        }
                        catch (Exception exception)
                        {
                            _logger.LogError(exception, "Failed to delete reward {RewardId}", rewardId);
                        }
                    }
                }
            }
        }
    }
}
