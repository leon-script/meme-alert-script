using MediatR;
using TwitchLeonScript.Application.Common;

namespace TwitchLeonScript.Application.Queries.TwitchState
{
    public sealed class GetTwitchStateQuery : IRequest<Result<GetTwitchStateResponse>>
    {
    }
}
