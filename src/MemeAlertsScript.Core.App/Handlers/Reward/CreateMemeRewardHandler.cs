using MediatR;
using MemeAlertsScript.Core.App.Commands.Reward;
using MemeAlertsScript.Core.App.Responses.Reward;
using MemeAlertsScript.Core.Common.Extensions;
using MemeAlertsScript.Core.Twitch.Services;

namespace MemeAlertsScript.Core.App.Handlers.Reward
{
    public class CreateMemeRewardHandler(
        TwitchRewardService twitchRewardService)
        : IRequestHandler<CreateMemeRewardCommand, CreateMemeRewardResponse>
    {
        public readonly TwitchRewardService twitchRewardService = twitchRewardService;

        public async Task<CreateMemeRewardResponse> Handle(CreateMemeRewardCommand request, CancellationToken cancellationToken)
        {
            var reward = await this.twitchRewardService.CreateCustomRewardAsync(
                oauthToken: request.OAuthToken,
                broadcasterId: request.BroadcasterId,
                title: request.Title,
                prompt: request.Prompt + "\n" + request.MemeCost.ToMemeTag(),
                twitchCost: request.TwitchCost,
                isEnabled: true,
                isUserInputRequired: true);

            return new CreateMemeRewardResponse 
            { 
                Reward = reward 
            };
        }
    }
}
