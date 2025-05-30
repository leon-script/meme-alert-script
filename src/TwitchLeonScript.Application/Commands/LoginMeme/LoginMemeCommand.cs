using MediatR;
using TwitchLeonScript.Application.Common;

namespace TwitchLeonScript.Application.Commands.LoginMeme
{
    public sealed class LoginMemeCommand : IRequest<Result<LoginMemeResponse>>
    {
        public string? OAuthToken { get; init; }
        public string? RefreshToken { get; init; }
        public DateTime? ExpiresAt { get; init; }
    }
}
