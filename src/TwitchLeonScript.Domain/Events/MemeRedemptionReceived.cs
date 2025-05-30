using MediatR;
using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Events
{
    public sealed class MemeRedemptionReceived : INotification
    {
        public required TwitchRedemption Redemption { get; init; }
    }
}
