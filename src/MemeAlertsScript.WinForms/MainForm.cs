using MemeAlertsScript.Core.Models;
using MemeAlertsScript.Twitch;
using MemeAlertsScript.WinForms.Configs;

namespace MemeAlertsScript.WinForms
{
    public partial class MainForm : Form
    {
        private TwitchSettings _config;
        private EventSubListener _eventSub;
        private TwitchAuthTokens? _authTokens;
        private TwitchBroadcaster? _broadcaster;

        public MainForm()
        {
            _config = Configuration.LoadTwitchSettings();
            InitializeComponent();
        }

        private async void MainForm_LoadAsync(object sender, EventArgs e)
        {

        }

        private async void MainForm_FormClosing(object sender, EventArgs e)
        {
            if (_eventSub != null)
            {
                _eventSub.OnRewardRedeemed -= OnRewardRedeemed;
                await _eventSub.StopAsync(CancellationToken.None);
            }
        }

        private async void buttonTwitchLogin_Click(object sender, EventArgs e)
        {
            using var oauthForm = new OAuthForm(_config);
            var dialogResult = oauthForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                _authTokens = oauthForm.AuthTokensResult;
                _broadcaster = oauthForm.BroadcasterResult;
                textBoxTwitchLogin.Text = _broadcaster!.Login!;
                buttonTwitchLogin.Enabled = false;
                buttonTwitchLogout.Enabled = true;

                var tokenPreview = _authTokens!.AccessToken?.Length >= 12
                    ? _authTokens.AccessToken[..6] + "..." + _authTokens.AccessToken[^6..]
                    : _authTokens.AccessToken ?? "";
                logsRichTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Twitch OAuth success ({tokenPreview}).\n");

                var appToken = await TwitchTokenHelper.GetAppTokenAsync(_config.AppId, _config.AppSecret);
                _eventSub = new EventSubListener(_config.AppId, appToken!, _broadcaster.Id!, _authTokens.AccessToken!);
                _eventSub.OnRewardRedeemed += OnRewardRedeemed;
                await _eventSub.StartAsync(CancellationToken.None);
            }
            else
            {
                _authTokens = null;
                _broadcaster = null;
                textBoxTwitchLogin.Text = string.Empty;
                buttonTwitchLogin.Enabled = true;
                buttonTwitchLogout.Enabled = false;
                logsRichTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Twitch OAuth cancelled.\n");
                
                if (_eventSub != null)
                {
                    _eventSub.OnRewardRedeemed -= OnRewardRedeemed;
                    await _eventSub.StopAsync(CancellationToken.None);
                }
            }
        }

        private async void buttonTwitchLogout_Click(object sender, EventArgs e)
        {
            _authTokens = null;
            _broadcaster = null;
            textBoxTwitchLogin.Text = string.Empty;
            buttonTwitchLogin.Enabled = true;
            buttonTwitchLogout.Enabled = false;
            logsRichTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Twitch OAuth logout.\n");

            if (_eventSub != null)
            {
                _eventSub.OnRewardRedeemed -= OnRewardRedeemed;
                await _eventSub.StopAsync(CancellationToken.None);
            }
        }

        private void OnRewardRedeemed(string userName, string rewardTitle)
        {
            if (logsRichTextBox.InvokeRequired)
            {
                logsRichTextBox.Invoke(new Action(() =>
                    logsRichTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Reward redeemed: {userName} - {rewardTitle}.\n")
                ));
            }
            else
            {
                logsRichTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Reward redeemed: {userName} - {rewardTitle}.\n");
            }
        }
    }
}
