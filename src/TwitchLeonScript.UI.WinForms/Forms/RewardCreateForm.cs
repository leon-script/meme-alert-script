using MediatR;
using TwitchLeonScript.Core.App.Commands.MemeReward;
using TwitchLeonScript.Core.Meme.Models;
using TwitchLeonScript.UI.Tokens.Infrastructure;

namespace TwitchLeonScript.UI.WinForms.Forms
{
    public partial class RewardCreateForm : Form
    {
        private readonly IMediator _mediator;
        private readonly ITwitchTokenStorage _tokenStorage;

        public RewardCreateForm(IMediator mediator, ITwitchTokenStorage tokenStorage)
        {
            _mediator = mediator;
            _tokenStorage = tokenStorage;

            InitializeComponent();
        }

        private void RewardCreateForm_Load(object sender, EventArgs e)
        {
            nudTwitchPoints.Value = 500;
            nudBonus.Value = 10;
            txtTitle.Text = $"{nudBonus.Value} meme points memealerts.com";
            rtbPrompt.Text = "Enter your nickname from memealert. And also you should take the starting bonus for the first time via the streamer's link to become a supporter of the streamer.";
        }

        private async void btnCreateMemeReward_Click(object sender, EventArgs e)
        {
            var storedToken = _tokenStorage.Load();

            if (storedToken == null)
            {
                MessageBox.Show("Twitch token not found. Please login to Twitch first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
                Close();
                return;
            }

            // TODO CHECK IF THE USER IS EXISTS

            var memeReward = new MemeRewardDto
            {
                BroadcasterId = storedToken.BroadcasterId,
                Title = txtTitle.Text,
                Prompt = rtbPrompt.Text,
                TwitchCost = (int)nudTwitchPoints.Value,
                MemeCost = (int)nudBonus.Value,
            };

            var command = new CreateMemeRewardCommand
            {
                OAuthToken = storedToken.OAuthToken,
                MemeReward = memeReward,
            };
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                MessageBox.Show("Failed to create meme reward. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
                Close();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
