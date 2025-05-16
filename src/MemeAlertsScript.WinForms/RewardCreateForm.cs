using MemeAlertsScript.Core.Models;

namespace MemeAlertsScript.WinForms
{
    public partial class CreateRewardForm : Form
    {
        public CreateMemeReward? RewardResult { get; private set; }

        public CreateRewardForm()
        {
            InitializeComponent();
        }

        private void CreateRewardForm_Load(object sender, EventArgs e)
        {
            nameTextBox.Text = "Buy 10 MemeAlerts Points";
            promptRichTextBox.Text = "Exchange your Twitch Channel Points for MemeAlerts points. You must have already sent points to MemeAlerts. Enter your MemeAlerts username below.";
            twitchCostNumericUpDown.Value = 1000;
            memeCostNumericUpDown.Value = 10;
        }

        private void createRewardButton_Click(object sender, EventArgs e)
        {
            RewardResult = new CreateMemeReward
            {
                Name = nameTextBox.Text,
                Prompt = $"[{(int)memeCostNumericUpDown.Value}] {promptRichTextBox.Text}",
                TwitchCost = (int)twitchCostNumericUpDown.Value,
                MemeCost = (int)memeCostNumericUpDown.Value,
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
