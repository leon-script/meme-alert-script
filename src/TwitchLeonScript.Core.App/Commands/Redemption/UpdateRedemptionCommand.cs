using MediatR;
using TwitchLeonScript.Core.App.Responses.Redemption;
using TwitchLib.Api.Core.Enums;

namespace TwitchLeonScript.Core.App.Commands.Redemption
{
    public class UpdateRedemptionCommand : IRequest<UpdateRedemptionResponse>
    {
        public required string OAauthToken { get; init; }
        public required string BroadcasterId { get; init; }
        public required string RewardId { get; init; }
        public required string RedemptionId { get; init; }
        public required CustomRewardRedemptionStatus Status { get; init; }
    }
}
