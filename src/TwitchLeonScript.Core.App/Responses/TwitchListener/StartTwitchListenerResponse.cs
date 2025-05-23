using MediatR;

namespace TwitchLeonScript.Core.App.Responses.TwitchListener
{
    public class StartTwitchListenerResponse
    {
        public required Unit Unit { get; init; }
    }
}
