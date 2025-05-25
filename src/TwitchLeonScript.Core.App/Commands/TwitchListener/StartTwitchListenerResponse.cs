using MediatR;

namespace TwitchLeonScript.Core.App.Commands.TwitchListener
{
    public sealed class StartTwitchListenerResponse
    {
        public required bool IsSuccess { get; init; }
        public Unit Unit { get; init; }
    }
}
