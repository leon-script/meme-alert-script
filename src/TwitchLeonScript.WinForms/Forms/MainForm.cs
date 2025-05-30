using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;
using TwitchLeonScript.Application.Extensions;
using TwitchLeonScript.Domain.Events;
using TwitchLeonScript.Domain.Models;
using TwitchLeonScript.Session.Storages;
using TwitchLeonScript.WinForms.Models;
using TwitchLeonScript.WinForms.Wrappers;

namespace TwitchLeonScript.WinForms.Forms
{
    internal partial class MainForm : Form,
        INotificationHandler<MemeRedemptionReceived>,
        INotificationHandler<MemeRedemptionResolved>,
        INotificationHandler<MemeSupporterNotFound>,
        INotificationHandler<MemeRedemptionErrorHandling>,
        INotificationHandler<RedemptionErrorHandling>
    {
        private readonly IServiceProvider _provider;
        private readonly TwitchSessionStorage _twitchTokenStorage;
        private readonly MemeSessionStorage _memeTokenStorage;
        private readonly MemeRedemptionsGridWrapper _memeRedemptionsGridWrapper;

        public MainForm(IServiceProvider provider, TwitchSessionStorage twitchTokenStorage, MemeSessionStorage memeTokenStorage)
        {
            _provider = provider;
            _twitchTokenStorage = twitchTokenStorage;
            _memeTokenStorage = memeTokenStorage;

            InitializeComponent();

            _memeRedemptionsGridWrapper = new MemeRedemptionsGridWrapper(dgvMemeRedemptions);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Text += $@" v{version!.Major}.{version.Minor}";

            TwitchLogin();
            MemeLogin();
        }

        private void MainForm_FormClosing(object sender, EventArgs e)
        {

        }

        Task INotificationHandler<MemeRedemptionReceived>.Handle(MemeRedemptionReceived notification, CancellationToken cancellationToken)
        {
            var redemptionRow = CreateRedemptionRow(notification.Redemption);
            var rowIndex = _memeRedemptionsGridWrapper.AddNewRow(redemptionRow);

            var status = "In progress";
            _memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, status);

            return Task.CompletedTask;
        }

        Task INotificationHandler<MemeRedemptionResolved>.Handle(MemeRedemptionResolved notification, CancellationToken cancellationToken)
        {
            var rowIndex = _memeRedemptionsGridWrapper.FindRowIndexByRedemptionId(notification.Redemption.Id);

            if (rowIndex == -1)
            {
                var redemptionRow = CreateRedemptionRow(notification.Redemption);
                rowIndex = _memeRedemptionsGridWrapper.AddNewRow(redemptionRow);
            }

            var status = string.Format("Bonus {0} points sent", notification.Redemption.Reward.Prompt.ParseMemeTagNumber());
            _memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, status);
            _memeRedemptionsGridWrapper.ResolveRow(rowIndex);

            return Task.CompletedTask;
        }

        Task INotificationHandler<MemeSupporterNotFound>.Handle(MemeSupporterNotFound notification, CancellationToken cancellationToken)
        {
            var rowIndex = _memeRedemptionsGridWrapper.FindRowIndexByRedemptionId(notification.Redemption.Id);

            if (rowIndex == -1)
            {
                var redemptionRow = CreateRedemptionRow(notification.Redemption);
                rowIndex = _memeRedemptionsGridWrapper.AddNewRow(redemptionRow);
            }

            var status = string.Format("User '{0}' not found, bonus returned", notification.Redemption.Input);
            _memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, status);
            _memeRedemptionsGridWrapper.DeclineRow(rowIndex);

            return Task.CompletedTask;
        }

        Task INotificationHandler<MemeRedemptionErrorHandling>.Handle(MemeRedemptionErrorHandling notification, CancellationToken cancellationToken)
        {
            var rowIndex = _memeRedemptionsGridWrapper.FindRowIndexByRedemptionId(notification.Redemption.Id);

            if (rowIndex == -1)
            {
                var redemptionRow = CreateRedemptionRow(notification.Redemption);
                rowIndex = _memeRedemptionsGridWrapper.AddNewRow(redemptionRow);
            }

            var status = string.Format("Error: {0}", notification.Reason);
            _memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, status);
            _memeRedemptionsGridWrapper.DeclineRow(rowIndex);

            return Task.CompletedTask;
        }

        Task INotificationHandler<RedemptionErrorHandling>.Handle(RedemptionErrorHandling notification, CancellationToken cancellationToken)
        {
            var rowIndex = _memeRedemptionsGridWrapper.FindRowIndexByRedemptionId(notification.Redemption.Id);

            if (rowIndex == -1)
            {
                ShowError("Unexpected error during redemption handling.", notification.Reason);
                return Task.CompletedTask;
            }

            var redemptionRow = CreateRedemptionRow(notification.Redemption);
            rowIndex = _memeRedemptionsGridWrapper.AddNewRow(redemptionRow);

            var status = string.Format("Error: {0}", notification.Reason);
            _memeRedemptionsGridWrapper.UpdateRowStatus(rowIndex, status);
            _memeRedemptionsGridWrapper.DeclineRow(rowIndex);

            return Task.CompletedTask;
        }

        private static MemeRedemptionGridRow CreateRedemptionRow(TwitchRedemption redemption)
        {
            return new MemeRedemptionGridRow()
            {
                RedemptionId = redemption.Id,
                RewardId = redemption.Reward.Id,
                Status = string.Empty,
                Time = redemption.RedeemedAt.ToString("HH:mm:ss.fff"),
                TwitchUsername = redemption.User.Login,
                MemeUsername = redemption.Input,
                MemeBonus = redemption.Reward.Prompt.ParseMemeTagNumber(),
                RewardTitle = redemption.Reward.Title
            };
        }

        private static void ShowError(string message, string? errorMessage = null)
        {
            MessageBox.Show($"{message} Please try again.\n\n{errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TwitchLogin()
        {
            using var twitchAuthForm = _provider.GetRequiredService<TwitchAuthForm>();

            var dialogResult = twitchAuthForm.ShowDialog(this);
            if (dialogResult != DialogResult.OK)
            {
                TwitchLogout();
                return;
            }

            var storedToken = _twitchTokenStorage.Load();
            if (storedToken is null)
            {
                TwitchLogout();
                return;
            }

            txtTwitchLogin.Text = $@"{storedToken.BroadcasterLogin} (ID: {storedToken.BroadcasterId})";
            btnTwitchLogin.Enabled = false;
            btnTwitchLogout.Enabled = true;
            btnTwitchRewardsEdit.Enabled = true;
            btnMemeRewardCreate.Enabled = true;
        }

        private void TwitchLogout()
        {
            _twitchTokenStorage.Clear();

            txtTwitchLogin.Text = string.Empty;
            btnTwitchLogin.Enabled = true;
            btnTwitchLogout.Enabled = false;
            btnTwitchRewardsEdit.Enabled = false;
            btnMemeRewardCreate.Enabled = false;
        }

        private void MemeLogin()
        {
            using var memeAuthForm = _provider.GetRequiredService<MemeAuthForm>();

            var dialogResult = memeAuthForm.ShowDialog(this);
            if (dialogResult != DialogResult.OK)
            {
                MemeLogout();
                return;
            }

            var storedToken = _memeTokenStorage.Load();
            if (storedToken is null)
            {
                MemeLogout();
                return;
            }

            txtMemeLogin.Text = $@"{storedToken.BroadcasterLogin} (ID: {storedToken.BroadcasterId})";
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

        private void btnTwitchLogin_Click(object sender, EventArgs e)
        {
            TwitchLogin();
        }

        private void btnTwitchLogout_Click(object sender, EventArgs e)
        {
            TwitchLogout();
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
                FileName = $"https://dashboard.twitch.tv/u/{storedToken.BroadcasterLogin}/viewer-rewards/channel-points/rewards",
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
