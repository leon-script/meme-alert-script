using MediatR;
using TwitchLeonScript.Core.App.Responses.Reward;

namespace TwitchLeonScript.Core.App.Commands.Reward
{
    public class CreateMemeRewardCommand : IRequest<CreateMemeRewardResponse>
    {
        public required string OAuthToken { get; init; }
        public required string BroadcasterId { get; init; }
        public required string Title { get; init; }
        public required string Prompt { get; init; }
        public required int TwitchCost { get; init; }
        public required int MemeCost { get; init; }
    }
}
