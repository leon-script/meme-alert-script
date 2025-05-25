using MediatR;
using TwitchLeonScript.Core.Twitch.Models;

namespace TwitchLeonScript.Core.App.Commands.TwitchRedemption
{
    public sealed class UpdateTwitchRedemptionCommand : IRequest<UpdateTwitchRedemptionResponse>
    {
        public string? OAuthToken { get; init; }
        public TwitchRedemptionDto? TwitchRedemption { get; init; }
    }
}
