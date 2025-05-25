using AutoMapper;
using MediatR;
using TwitchLeonScript.Core.Twitch.Services;
using TwitchLib.Api.Core.Enums;

namespace TwitchLeonScript.Core.App.Commands.TwitchRedemption
{
    public sealed class UpdateTwitchRedemptionHandler(
        IMapper mapper,
        TwitchRedemptionService twitchRedemptionService)
        : IRequestHandler<UpdateTwitchRedemptionCommand, UpdateTwitchRedemptionResponse>
    {
        public async Task<UpdateTwitchRedemptionResponse> Handle(UpdateTwitchRedemptionCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);
            ArgumentNullException.ThrowIfNull(request.TwitchRedemption);

            var customRedemptionStatus = mapper.Map<CustomRewardRedemptionStatus>(request.TwitchRedemption.Status);
            var redemptionId = await twitchRedemptionService.UpdateRedemptionStatusAsync(request.OAuthToken, request.TwitchRedemption);

            return new UpdateTwitchRedemptionResponse
            {
                IsSuccess = redemptionId != null,
                RedemptionId = redemptionId,
            };
        }
    }
}
