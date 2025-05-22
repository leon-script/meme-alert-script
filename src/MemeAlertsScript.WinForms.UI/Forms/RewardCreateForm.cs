using MediatR;
using MemeAlertsScript.Core.App.Commands.Reward;
using TwitchLeonScript.WinForms.Tokens.Infrastructure;

namespace MemeAlertsScript.WinForms
{
    public partial class RewardCreateForm : Form
    {
        private readonly IMediator mediator;
        private readonly ITwitchTokenStorage tokenStorage;

        public RewardCreateForm(IMediator mediator, ITwitchTokenStorage tokenStorage)
        {
            InitializeComponent();
            this.mediator = mediator;
            this.tokenStorage = tokenStorage;
        }

        private void RewardCreateForm_Load(object sender, EventArgs e)
        {
            this.nudTwitchPoints.Value = 500;
            this.nudBonus.Value = 10;
            this.txtTitle.Text = $"{this.nudBonus.Value} meme points memealerts.com";
            this.rtbPrompt.Text = "Enter your nickname from memealert. And also you should take the starting bonus for the first time via the streamer's link to become a supporter of the streamer.";
        }

        private async void btnCreateMemeReward_Click(object sender, EventArgs e)
        {
            var storedToken = this.tokenStorage.Load();
            if (storedToken == null)
            {
                MessageBox.Show("Twitch token not found. Please login to Twitch first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
                Close();
                return;
            }

            // TODO CHECK IF THE USER IS EXISTS

            var command = new CreateMemeRewardCommand
            {
                OAuthToken = storedToken.OAuthToken,
                BroadcasterId = storedToken.BroadcasterId,
                Title = this.txtTitle.Text,
                Prompt = this.rtbPrompt.Text,
                TwitchCost = (int)this.nudTwitchPoints.Value,
                MemeCost = (int)this.nudBonus.Value

            };
            var response = await this.mediator.Send(command);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
