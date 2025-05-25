using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;
using TwitchLeonScript.Core.App.Commands.MemeBonus;
using TwitchLeonScript.Core.App.Commands.TwitchListener;
using TwitchLeonScript.Core.App.Commands.TwitchRedemption;
using TwitchLeonScript.Core.App.Queries.MemeSupporters;
using TwitchLeonScript.Core.Common.Enums;
using TwitchLeonScript.Core.Common.Extensions;
using TwitchLeonScript.Core.Meme.Models;
using TwitchLeonScript.Core.Twitch.Models;
using TwitchLeonScript.Core.Twitch.Notifications;
using TwitchLeonScript.UI.Common;
using TwitchLeonScript.UI.Tokens.Infrastructure;
using TwitchLeonScript.UI.WinForms.Models;
using TwitchLeonScript.UI.WinForms.Wrappers;

namespace TwitchLeonScript.UI.WinForms.Forms
{
    internal partial class MainForm : Form, INotificationHandler<TwitchRedemptionReceived>
    {
        private readonly IServiceProvider _provider;
        private readonly IMediator _mediator;
        private readonly ITwitchTokenStorage _twitchTokenStorage;
        private readonly IMemeTokenStorage _memeTokenStorage;
        private readonly MemeRedemptionsGridWrapper _memeRedemptionsGridWrapper;

        public MainForm(IServiceProvider provider, IMediator mediator, ITwitchTokenStorage twitchTokenStorage, IMemeTokenStorage memeTokenStorage)
        {
            _provider = provider;
            _mediator = mediator;
            _twitchTokenStorage = twitchTokenStorage;
            _memeTokenStorage = memeTokenStorage;
            _memeRedemptionsGridWrapper = new MemeRedemptionsGridWrapper(dgvMemeRedemptions);

            InitializeComponent();
        }

        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Text += $@" v{version!.Major}.{version.Minor}";

            await TwitchLoginAsync();
            MemeLogin();
        }

        private async void MainForm_FormClosing(object sender, EventArgs e)
        {
            var command = new StopTwitchListenerCommand();
            var response = await _mediator.Send(command);
        }

        async Task INotificationHandler<TwitchRedemptionReceived>.Handle(TwitchRedemptionReceived notification, CancellationToken cancellationToken)
        {
            //notification.Redemption;
            // TODO MAPPING
            // TODO SWITCH
            // TODO ArgumentNullException
            // PARSE MEME TAG CHECK

            var storedTwitchToken = _twitchTokenStorage.Load();

            if (storedTwitchToken == null)
            {
                throw new ArgumentNullException(nameof(storedTwitchToken));
            }

            var storedMemeToken = _memeTokenStorage.Load();

            if (storedMemeToken == null)
            {
                throw new ArgumentNullException(nameof(storedMemeToken));
            }

            var redemptionRow = new MemeRedemptionGridRow()
            {
                RedemptionId = notification.Redemption.Id,
                RewardId = notification.Redemption.Reward.Id,
                Status = MemeRedemptionStatus.Init,
                Time = notification.Redemption.RedeemedAt.ToString("HH:mm:ss.fff"),
                TwitchUsername = notification.Redemption.UserName,
                MemeUsername = notification.Redemption.UserInput,
                MemeBonus = notification.Redemption.Reward.Prompt.ParseMemeTagNumber(),
                RewardTitle = notification.Redemption.Reward.Title
            };

            await ResolveMemeRedemption(
                twitchOAuthToken: storedTwitchToken.OAuthToken,
                twitchBroadcasterId: storedTwitchToken.BroadcasterId,
                memeOAuthToken: storedMemeToken.OAuthToken,
                memeBroadcasterId: storedMemeToken.BroadcasterId,
                rowIndex: _memeRedemptionsGridWrapper.AddNewRow(redemptionRow),
                rewardId: notification.Redemption.Reward.Id,
                redemptionId: notification.Redemption.Id,
                memeUsername: notification.Redemption.UserInput,
                bonus: notification.Redemption.Reward.Prompt.ParseMemeTagNumber());
        }

        private async Task ResolveMemeRedemption(
            string twitchOAuthToken,
            string twitchBroadcasterId,
            string memeOAuthToken,
            string memeBroadcasterId,
            int rowIndex,
            string rewardId,
            string redemptionId,
            string memeUsername,
            int bonus)
        {
            _memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, MemeRedemptionStatus.InProgress);

            var supportersCommand = new GetMemeSupportersCommand { OAuthToken = memeOAuthToken };
            var memeSupportersResponse = await _mediator.Send(supportersCommand);

            var memeSupporter = memeSupportersResponse.Supporters.FirstOrDefault(x => x.Name.Equals(memeUsername));
            if (memeSupporter == null)
            {
                await DeclineMemeRedemption(twitchOAuthToken, twitchBroadcasterId, rowIndex, rewardId, redemptionId, MemeRedemptionStatus.UserNotFound);
            }
            else
            {
                var bonusCommand = new SendMemeBonusCommand
                {
                    OAuthToken = memeOAuthToken,
                    MemeBonus = new MemeBonusDto
                    {
                        UserId = memeSupporter.Id,
                        BroadcasterId = memeBroadcasterId,
                        Value = bonus,
                    },
                };
                await _mediator.Send(bonusCommand);

                var resolveCommand = new UpdateTwitchRedemptionCommand
                {
                    OAuthToken = twitchOAuthToken,
                    TwitchRedemption = new TwitchRedemptionDto
                    {
                        BroadcasterId = twitchBroadcasterId,
                        RedemptionId = redemptionId,
                        RewardId = rewardId,
                        Status = TwitchRewardRedemptionStatus.Fulfilled,
                    },
                };
                await _mediator.Send(resolveCommand);

                _memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, string.Format(MemeRedemptionStatus.Resolved, bonus));
                _memeRedemptionsGridWrapper.ResolveRow(rowIndex);
            }
        }

        private async Task DeclineMemeRedemption(
            string twitchOAuthToken,
            string twitchBroadcasterId,
            int rowIndex,
            string rewardId,
            string redemptionId,
            string status)
        {
            var declineCommand = new UpdateTwitchRedemptionCommand
            {
                OAuthToken = twitchOAuthToken,
                TwitchRedemption = new TwitchRedemptionDto
                {
                    BroadcasterId = twitchBroadcasterId,
                    RedemptionId = redemptionId,
                    RewardId = rewardId,
                    Status = TwitchRewardRedemptionStatus.Canceled,
                },
            };
            await _mediator.Send(declineCommand);

            _memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, status);
            _memeRedemptionsGridWrapper.DeclineRow(rowIndex);
        }

        private async Task TwitchLoginAsync()
        {
            var storedToken = _twitchTokenStorage.Load();
            if (storedToken == null)
            {
                using var twitchAuthForm = _provider.GetRequiredService<TwitchAuthForm>();
                var dialogResult = twitchAuthForm.ShowDialog(this);

                if (dialogResult != DialogResult.OK)
                {
                    await TwitchLogoutAsync();
                    return;
                }

                storedToken = _twitchTokenStorage.Load();
                if (storedToken == null)
                {
                    await TwitchLogoutAsync();
                    return;
                }
            }

            txtTwitchLogin.Text = $@"{storedToken.BroadcasterName} (ID: {storedToken.BroadcasterId})";
            btnTwitchLogin.Enabled = false;
            btnTwitchLogout.Enabled = true;
            btnTwitchRewardsEdit.Enabled = true;
            btnMemeRewardCreate.Enabled = true;

            var command = new StartTwitchListenerCommand
            {
                AccessToken = storedToken.AccessToken,
                OAuthToken = storedToken.OAuthToken,
                BroadcasterId = storedToken.BroadcasterId
            };
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                MessageBox.Show("Failed to start Twitch listener. Please check your credentials and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await TwitchLogoutAsync();
            }
        }

        private async Task TwitchLogoutAsync()
        {
            _twitchTokenStorage.Clear();

            txtTwitchLogin.Text = string.Empty;
            btnTwitchLogin.Enabled = true;
            btnTwitchLogout.Enabled = false;
            btnTwitchRewardsEdit.Enabled = false;
            btnMemeRewardCreate.Enabled = false;

            var command = new StopTwitchListenerCommand();
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                MessageBox.Show("Failed to stop Twitch listener. Please check your credentials and try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MemeLogin()
        {
            var storedToken = _memeTokenStorage.Load();
            if (storedToken == null)
            {
                using var memeAuthForm = _provider.GetRequiredService<MemeAuthForm>();
                var dialogResult = memeAuthForm.ShowDialog(this);

                if (dialogResult != DialogResult.OK)
                {
                    MemeLogout();
                    return;
                }

                storedToken = _memeTokenStorage.Load();
                if (storedToken == null)
                {
                    MemeLogout();
                    return;
                }
            }

            txtMemeLogin.Text = $@"{storedToken.BroadcasterName} (ID: {storedToken.BroadcasterId})";
            btnMemeLogin.Enabled = false;
            btnMemeLogout.Enabled = true;
        }

        private void MemeLogout()
        {
            _memeTokenStorage.Clear();

            txtMemeLogin.Text = string.Empty;
            btnMemeLogin.Enabled = true;
            btnMemeLogout.Enabled = false;
        }

        private async void btnTwitchLogin_Click(object sender, EventArgs e)
        {
            await TwitchLoginAsync();
        }

        private async void btnTwitchLogout_Click(object sender, EventArgs e)
        {
            await TwitchLogoutAsync();
        }

        private void btnMemeLogin_Click(object sender, EventArgs e)
        {
            MemeLogin();
        }

        private void btnMemeLogout_Click(object sender, EventArgs e)
        {
            MemeLogout();
        }

        private void btnRedemptionsClear_Click(object sender, EventArgs e)
        {
            _memeRedemptionsGridWrapper.ClearRows();
        }

        private void btnTwitchRewardsEdit_Click(object sender, EventArgs e)
        {
            var storedToken = _twitchTokenStorage.Load();

            if (storedToken is null)
            {
                MessageBox.Show("Twitch token not found. Please login to Twitch first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://dashboard.twitch.tv/u/{storedToken.BroadcasterName}/viewer-rewards/channel-points/rewards",
                UseShellExecute = true
            });
        }

        private void btnMemeRewardCreate_Click(object sender, EventArgs e)
        {
            var createRewardForm = _provider.GetRequiredService<RewardCreateForm>();
            createRewardForm.ShowDialog(this);
        }
    }
}
