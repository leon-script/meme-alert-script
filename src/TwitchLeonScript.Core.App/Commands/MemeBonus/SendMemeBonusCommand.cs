using MediatR;
using TwitchLeonScript.Core.App.Models;

namespace TwitchLeonScript.Core.App.Commands.MemeBonus
{
    public sealed class SendMemeBonusCommand : IRequest<SendMemeBonusResponse>
    {
        public string? OAuthToken { get; init; }
        public MemeBonusDto? MemeBonus {  get; init; }
    }
}
