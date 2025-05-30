using MediatR;
using TwitchLeonScript.Application.Commands.MemeReward;
using TwitchLeonScript.Session.Storages;

namespace TwitchLeonScript.WinForms.Forms
{
    internal partial class RewardCreateForm : Form
    {
        private readonly IMediator _mediator;
        private readonly TwitchSessionStorage _tokenStorage;

        public RewardCreateForm(IMediator mediator, TwitchSessionStorage tokenStorage)
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
            try
            {
                var storedToken = _tokenStorage.Load();
                if (storedToken is null)
                {
                    ShowError("Twitch token not found.");
                    CloseForm(DialogResult.Abort);
                    return;
                }

                var response = await _mediator.Send(new CreateMemeRewardCommand
                {
                    Title = txtTitle.Text,
                    Prompt = rtbPrompt.Text,
                    TwitchCost = (int)nudTwitchPoints.Value,
                    MemeCost = (int)nudBonus.Value,
                });

                if (!response.IsSuccess || response.Value is null)
                {
                    ShowError("Failed to create meme reward.", response.Error);
                    CloseForm(DialogResult.Abort);
                    return;
                }

                CloseForm(DialogResult.OK);
            }
            catch (Exception ex)
            {
                ShowError("Unexpected error during reward creation.", ex.Message);
                CloseForm(DialogResult.Abort);
            }
        }

        private static void ShowError(string message, string? errorMessage = null)
        {
            MessageBox.Show($"{message} Please try again.\n\n{errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CloseForm(DialogResult result)
        {
            BeginInvoke(() =>
            {
                DialogResult = result;
                Close();
            });
        }
    }
}
