using MediatR;
using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Commands.MemeReward
{
    public sealed class CreateMemeRewardCommand : IRequest<CreateMemeRewardResponse>
    {
        public string? OAuthToken { get; init; }
        public MemeRewardDto? MemeReward { get; init; }
    }
}
