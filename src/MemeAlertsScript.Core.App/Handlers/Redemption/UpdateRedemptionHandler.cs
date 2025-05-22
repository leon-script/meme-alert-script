using MediatR;
using MemeAlertsScript.Core.App.Commands.Redemption;
using MemeAlertsScript.Core.App.Responses.Redemption;
using MemeAlertsScript.Core.Twitch.Services;

namespace MemeAlertsScript.Core.App.Handlers.Redemption
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
