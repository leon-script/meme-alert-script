namespace MemeAlertsScript.WinForms.UI.Models
{
    internal class MemeRedemptionGridRow
    {
        public required string RedemptionId { get; set; }
        public required string RewardId { get; set; }
        public required string Status { get; set; }
        public required string Time { get; set; }
        public required string TwitchUsername { get; set; }
        public required string MemeUsername { get; set; }
        public required int MemeBonus { get; set; }
        public required string RewardTitle { get; set; }
    }
}
