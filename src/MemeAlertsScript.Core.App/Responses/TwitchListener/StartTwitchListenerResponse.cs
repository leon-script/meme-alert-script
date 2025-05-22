using MediatR;

namespace MemeAlertsScript.Core.App.Responses.TwitchListener
{
    public class StartTwitchListenerResponse
    {
        public required Unit Unit { get; init; }
    }
}
