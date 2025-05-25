using MediatR;

namespace TwitchLeonScript.Core.App.Queries.TwitchAppToken
{
    public sealed class GetTwitchAppTokenCommand : IRequest<GetTwitchAppTokenResponse>
    {
    }
}
