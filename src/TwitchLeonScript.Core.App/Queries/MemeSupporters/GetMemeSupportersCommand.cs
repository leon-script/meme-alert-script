using MediatR;

namespace TwitchLeonScript.Core.App.Queries.MemeSupporters
{
    public sealed class GetMemeSupportersCommand : IRequest<GetMemeSupportersResponse>
    {
        public string? OAuthToken { get; init; }
    }
}
