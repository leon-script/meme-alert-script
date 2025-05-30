using TwitchLeonScript.Domain.Enums;

namespace TwitchLeonScript.Domain.Models
{
    public sealed class TwitchRedemption
    {
        public required string Id { get; init; }
        public required string Input { get; init; }
        public required RedemptionStatus Status { get; init; }
        public required DateTimeOffset RedeemedAt { get; init; }

        public required TwitchReward Reward { get; init; }
        public required TwitchBroadcaster Broadcaster { get; init; }
        public required TwitchUser User { get; init; }
    }
}
