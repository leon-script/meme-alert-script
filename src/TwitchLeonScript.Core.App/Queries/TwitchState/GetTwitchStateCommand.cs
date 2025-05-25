using MediatR;

namespace TwitchLeonScript.Core.App.Queries.TwitchState
{
    public sealed class GetTwitchStateCommand : IRequest<GetTwitchStateResponse>
    {
    }
}
