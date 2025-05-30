namespace TwitchLeonScript.WinForms.Models
{
    internal sealed class MemeRedemptionGridRow
    {
        public required string RedemptionId { get; init; }
        public required string RewardId { get; init; }
        public required string Status { get; init; }
        public required string Time { get; init; }
        public required string TwitchUsername { get; init; }
        public required string MemeUsername { get; init; }
        public required int MemeBonus { get; init; }
        public required string RewardTitle { get; init; }
    }
}
