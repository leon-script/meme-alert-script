using MediatR;
using MemeAlertsScript.Core.App.Commands.Bonus;
using MemeAlertsScript.Core.App.Commands.MemeAuth;
using MemeAlertsScript.Core.App.Commands.Redemption;
using MemeAlertsScript.Core.App.Commands.TwitchListener;
using MemeAlertsScript.Core.Common.Extensions;
using MemeAlertsScript.Core.Twitch.Notifications;
using MemeAlertsScript.WinForms.Services;
using MemeAlertsScript.WinForms.UI.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;
using TwitchLeonScript.WinForms.Common;
using TwitchLeonScript.WinForms.Tokens.Infrastructure;
using TwitchLib.Api.Core.Enums;

namespace MemeAlertsScript.WinForms.Forms
{
    internal partial class MainForm : Form, INotificationHandler<TwitchRedemptionReceived>
    {
        private readonly IServiceProvider provider;
        private readonly IMediator mediator;
        private readonly ITwitchTokenStorage twitchTokenStorage;
        private readonly IMemeTokenStorage memeTokenStorage;
        private readonly MemeRedemptionsGridWrapper memeRedemptionsGridWrapper;

        public MainForm(IServiceProvider provider, IMediator mediator, ITwitchTokenStorage twitchTokenStorage, IMemeTokenStorage memeTokenStorage)
        {
            InitializeComponent();

            this.provider = provider;
            this.mediator = mediator;
            this.twitchTokenStorage = twitchTokenStorage;
            this.memeTokenStorage = memeTokenStorage;
            this.memeRedemptionsGridWrapper = new MemeRedemptionsGridWrapper(this.dgvMemeRedemptions);
        }

        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text += $" v{version!.Major}.{version.Minor}";

            await TwitchLoginAsync();
            MemeLogin();
        }

        private async void MainForm_FormClosing(object sender, EventArgs e)
        {
            var command = new StopTwitchListenerCommand();
            var response = await this.mediator.Send(command);
        }

        async Task INotificationHandler<TwitchRedemptionReceived>.Handle(TwitchRedemptionReceived notification, CancellationToken cancellationToken)
        {
            //notification.Redemption;
            // TODO MAPPING
            // TODO SWITCH
            // TODO ArgumentNullException
            // PARSE MEME TAG CHECK

            var storedTwitchToken = this.twitchTokenStorage.Load();
            if (storedTwitchToken == null)
            {
                throw new ArgumentNullException(nameof(storedTwitchToken));
            }

            var storedMemeToken = this.memeTokenStorage.Load();
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
                rowIndex: this.memeRedemptionsGridWrapper.AddNewRow(redemptionRow),
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
            this.memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, MemeRedemptionStatus.InProgress);

            var supportesCommand = new GetMemeSupportersCommand { OAuthToken = memeOAuthToken };
            var memeSupportesResponse = await this.mediator.Send(supportesCommand);

            var memeSupporter = memeSupportesResponse.Supporters.FirstOrDefault(x => x.Name.Equals(memeUsername));
            if (memeSupporter == null)
            {
                await DeclineMemeRedemption(twitchOAuthToken, twitchBroadcasterId, rowIndex, rewardId, redemptionId, MemeRedemptionStatus.UserNotFound);
            }
            else
            {
                var bonusCommand = new SendMemeBonusCommand
                {
                    AccessToken = memeOAuthToken,
                    UserId = memeSupporter.Id,
                    StreamerId = memeBroadcasterId,
                    Value = bonus
                };
                await this.mediator.Send(bonusCommand);

                var resolveCommand = new UpdateRedemptionCommand
                {
                    OAauthToken = twitchOAuthToken,
                    BroadcasterId = twitchBroadcasterId,
                    RewardId = rewardId,
                    RedemptionId = redemptionId,
                    Status = CustomRewardRedemptionStatus.FULFILLED
                };
                await this.mediator.Send(resolveCommand);

                this.memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, string.Format(MemeRedemptionStatus.Resolved, bonus));
                this.memeRedemptionsGridWrapper.ResolveRow(rowIndex);
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
            var declineCommand = new UpdateRedemptionCommand
            {
                OAauthToken = twitchOAuthToken,
                BroadcasterId = twitchBroadcasterId,
                RewardId = rewardId!,
                RedemptionId = redemptionId!,
                Status = CustomRewardRedemptionStatus.CANCELED
            };
            await this.mediator.Send(declineCommand);

            this.memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, status);
            this.memeRedemptionsGridWrapper.DeclineRow(rowIndex);
        }

        private async Task TwitchLoginAsync()
        {
            var storedToken = this.twitchTokenStorage.Load();
            if (storedToken == null)
            {
                using var twitchAuthForm = this.provider.GetRequiredService<TwitchAuthForm>();

                var dialogResult = twitchAuthForm.ShowDialog(this);
                if (dialogResult != DialogResult.OK)
                {
                    await TwitchLogoutAsync();
                    return;
                }

                storedToken = this.twitchTokenStorage.Load();
                if (storedToken == null)
                {
                    await TwitchLogoutAsync();
                    return;
                }
            }

            this.txtTwitchLogin.Text = $"{storedToken.BroadcasterName} (ID: {storedToken.BroadcasterId})";
            this.btnTwitchLogin.Enabled = false;
            this.btnTwitchLogout.Enabled = true;
            this.btnTwitchRewardsEdit.Enabled = true;
            this.btnMemeRewardCreate.Enabled = true;

            var command = new StartTwitchListenerCommand
            {
                AccessToken = storedToken.AccessToken,
                OAuthToken = storedToken.OAuthToken,
                BroadcasterId = storedToken.BroadcasterId
            };
            var response = await this.mediator.Send(command);
        }

        private async Task TwitchLogoutAsync()
        {
            this.twitchTokenStorage.Clear();

            this.txtTwitchLogin.Text = string.Empty;
            this.btnTwitchLogin.Enabled = true;
            this.btnTwitchLogout.Enabled = false;
            this.btnTwitchRewardsEdit.Enabled = false;
            this.btnMemeRewardCreate.Enabled = false;

            var command = new StopTwitchListenerCommand();
            var response = await this.mediator.Send(command);
        }

        private void MemeLogin()
        {
            var storedToken = this.memeTokenStorage.Load();
            if (storedToken == null)
            {
                using var memeAuthForm = this.provider.GetRequiredService<MemeAuthForm>();

                var dialogResult = memeAuthForm.ShowDialog(this);
                if (dialogResult != DialogResult.OK)
                {
                    MemeLogout();
                    return;
                }

                storedToken = this.memeTokenStorage.Load();
                if (storedToken == null)
                {
                    MemeLogout();
                    return;
                }
            }

            this.txtMemeLogin.Text = $"{storedToken.BroadcasterName} (ID: {storedToken.BroadcasterId})";
            this.btnMemeLogin.Enabled = false;
            this.btnMemeLogout.Enabled = true;
        }

        private void MemeLogout()
        {
            this.memeTokenStorage.Clear();

            this.txtMemeLogin.Text = string.Empty;
            this.btnMemeLogin.Enabled = true;
            this.btnMemeLogout.Enabled = false;
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
            this.memeRedemptionsGridWrapper.ClearRows();
        }

        private void btnTwitchRewardsEdit_Click(object sender, EventArgs e)
        {
            var storedToken = this.twitchTokenStorage.Load();
            if (storedToken == null)
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
            var createRewardForm = this.provider.GetRequiredService<RewardCreateForm>();
            var dialogResult = createRewardForm.ShowDialog(this);
        }
    }
}
