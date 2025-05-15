using MemeAlertsScripts.Twitch;

namespace MemeAlertsScripts.WinForms
{
    public partial class MainForm : Form
    {
        private EventSubListener _eventSub;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {
            var clientId = ""; // TODO
            var clientSecret = ""; // TODO
            var userId = ""; // TODO
            var redirectUri = "http://localhost:54321/";
            var scopes = new[] { "channel:read:redemptions", "moderator:read:chat_settings" };

            var accessToken = await AuthApi.GetAppAccessTokenAsync(clientId, clientSecret)
                ?? throw new ArgumentNullException();

            var userToken = await AuthApi.GetOAuthAccessTokenAsync(clientId, clientSecret, redirectUri, scopes)
                ?? throw new ArgumentNullException();

            _eventSub = new EventSubListener(
                clientId: clientId,
                accessToken: accessToken,
                userId: userId,
                userToken: userToken
            );

            _eventSub.OnRewardRedeemed += OnRewardRedeemed;

            await _eventSub.StartAsync(CancellationToken.None);
        }

        private async void MainForm_FormClosing(object sender, EventArgs e)
        {
            await _eventSub.StopAsync(CancellationToken.None);
        }

        private void OnRewardRedeemed(string userName, string rewardTitle)
        {
            if (listBoxTwitch.InvokeRequired)
            {
                listBoxTwitch.Invoke(new Action(() =>
                    listBoxTwitch.Items.Add($"Reward redeemed: {userName} - {rewardTitle}")
                ));
            }
            else
            {
                listBoxTwitch.Items.Add($"Reward redeemed: {userName} - {rewardTitle}");
            }
        }
    }
}
