using MediatR;
using TwitchLeonScript.Application.Common;

namespace TwitchLeonScript.Application.Commands.MemeReward
{
    public sealed class CreateMemeRewardCommand : IRequest<Result<CreateMemeRewardResponse>>
    {
        public string? Title { get; init; }
        public int? TwitchCost { get; init; }
        public int? MemeCost { get; init; }
        public string? Prompt { get; init; }
    }
}
