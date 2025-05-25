using MediatR;

namespace TwitchLeonScript.Core.App.Queries.TwitchOAuth
{
    public sealed class GetTwitchOAuthCommand : IRequest<GetTwitchOAuthResponse>
    {
        public string? Code { get; init; }
    }
}
