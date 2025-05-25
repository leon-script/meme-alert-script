using MediatR;

namespace TwitchLeonScript.Core.App.Commands.TwitchListener
{
    public sealed class StartTwitchListenerCommand : IRequest<StartTwitchListenerResponse>
    {
        public string? AccessToken { get; init; }
        public string? OAuthToken { get; init; }
        public string? BroadcasterId { get; init; }
    }
}
