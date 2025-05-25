using MediatR;

namespace TwitchLeonScript.Core.App.Commands.TwitchListener
{
    public sealed class StopTwitchListenerCommand : IRequest<StopTwitchListenerResponse>
    {
    }
}
