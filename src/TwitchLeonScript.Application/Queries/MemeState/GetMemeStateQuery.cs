using MediatR;
using TwitchLeonScript.Application.Common;

namespace TwitchLeonScript.Application.Queries.MemeState
{
    public sealed class GetMemeStateQuery : IRequest<Result<GetMemeStateResponse>>
    {
        public required string Json { get; init; }
    }
}
