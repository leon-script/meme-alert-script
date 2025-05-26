using TwitchLeonScript.Core.App.Enums;

namespace TwitchLeonScript.Core.App.Models
{
    public sealed class TwitchRedemptionDto
    {
        public required string BroadcasterId { get; init; }
        public required string RedemptionId { get; init; }
        public required string RewardId { get; init; }
        public required TwitchRewardRedemptionStatus Status { get; init; }
    }
}
