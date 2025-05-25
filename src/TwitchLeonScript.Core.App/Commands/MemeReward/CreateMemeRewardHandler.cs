using MediatR;
using TwitchLeonScript.Core.Common.Extensions;
using TwitchLeonScript.Core.Twitch.Services;

namespace TwitchLeonScript.Core.App.Commands.MemeReward
{
    public class CreateMemeRewardHandler(
        TwitchRewardService twitchRewardService)
        : IRequestHandler<CreateMemeRewardCommand, CreateMemeRewardResponse>
    {
        public readonly TwitchRewardService _twitchRewardService = twitchRewardService;

        public async Task<CreateMemeRewardResponse> Handle(CreateMemeRewardCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);
            ArgumentNullException.ThrowIfNull(request.MemeReward);

            var customReward = await _twitchRewardService.CreateCustomRewardAsync(
                oauthToken: request.OAuthToken,
                broadcasterId: request.MemeReward.BroadcasterId,
                title: request.MemeReward.Title,
                prompt: request.MemeReward.Prompt + "\n" + request.MemeReward.MemeCost.ToMemeTag(),
                twitchCost: request.MemeReward.TwitchCost,
                isEnabled: true,
                isUserInputRequired: true);

            if (customReward == null)
            {
                return new CreateMemeRewardResponse { IsSuccess = false };
            }

            return new CreateMemeRewardResponse
            {
                IsSuccess = true,
                MemeRewardId = customReward.Id,
            };
        }
    }
}
