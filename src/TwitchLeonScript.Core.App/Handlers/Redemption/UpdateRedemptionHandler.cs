using MediatR;
using TwitchLeonScript.Core.App.Commands.Redemption;
using TwitchLeonScript.Core.App.Responses.Redemption;
using TwitchLeonScript.Core.Twitch.Services;

namespace TwitchLeonScript.Core.App.Handlers.Redemption
{
    public class UpdateRedemptionHandler(
        TwitchRedemptionService twitchRedemptionService)
        : IRequestHandler<UpdateRedemptionCommand, UpdateRedemptionResponse>
    {
        private readonly TwitchRedemptionService _twitchRedemptionService = twitchRedemptionService;

        public async Task<UpdateRedemptionResponse> Handle(UpdateRedemptionCommand request, CancellationToken cancellationToken)
        {
            var redemptionStatus = await _twitchRedemptionService.UpdateSingleRedemptionStatusAsync(
                oauthToken: request.OAauthToken,
                broadcasterId: request.BroadcasterId,
                rewardId: request.RewardId,
                redemptionId: request.RedemptionId,
                status: request.Status
            );

            return new UpdateRedemptionResponse
            {
                StatusResponse = redemptionStatus
            };
        }
    }
}
