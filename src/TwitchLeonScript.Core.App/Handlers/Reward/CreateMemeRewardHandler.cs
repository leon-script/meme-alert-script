using MediatR;
using TwitchLeonScript.Core.App.Commands.Reward;
using TwitchLeonScript.Core.App.Responses.Reward;
using TwitchLeonScript.Core.Common.Extensions;
using TwitchLeonScript.Core.Twitch.Services;

namespace TwitchLeonScript.Core.App.Handlers.Reward
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
