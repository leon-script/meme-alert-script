using MemeAlertsScript.Twitch;
using MemeAlertsScript.WinForms.Configs;

namespace MemeAlertsScript.WinForms
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
            var config = Configuration.LoadTwitchSettings();

            var appToken = await TwitchApiHelper.GetAppTokenAsync(config.AppId, config.AppSecret) ?? throw new ArgumentNullException();
            var api = new TwitchApiWrapper(config.AppId, config.AppSecret, appToken, config.RedirectUri, config.Scopes);

            var broadcasterId = await api.GetUserIdByLoginAsync(config.BroadcasterName) ?? throw new ArgumentNullException(config.BroadcasterName);
            var oauthToken = await api.GetOAuthTokenAsync() ?? throw new ArgumentNullException();

            _eventSub = new EventSubListener(config.AppId, appToken, broadcasterId, oauthToken);
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
