using TwitchLeonScript.Core.Common.Enums;

namespace TwitchLeonScript.Core.Twitch.Models
{
    public sealed class TwitchRedemptionDto
    {
        public required string BroadcasterId { get; init; }
        public required string RedemptionId { get; init; }
        public required string RewardId { get; init; }
        public required TwitchRewardRedemptionStatus Status { get; init; }
    }
}
