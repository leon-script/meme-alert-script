using MediatR;
using TwitchLeonScript.Domain.Models;

namespace TwitchLeonScript.Domain.Events
{
    public sealed class MemeRedemptionErrorHandling : INotification
    {
        public required string Reason { get; init; }
        public required TwitchRedemption Redemption { get; init; }
    }
}
