using MediatR;
using TwitchLeonScript.Core.App.Responses.Bonus;

namespace TwitchLeonScript.Core.App.Commands.Bonus
{
    public class SendMemeBonusCommand : IRequest<SendMemeBonusResponse>
    {
        public required string AccessToken {  get; init; }
        public required string UserId { get; init; }
        public required string StreamerId { get; init; }
        public required int Value { get; init; }
    }
}
