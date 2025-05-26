using MediatR;
using System.Net;
using TwitchLeonScript.Core.App.Services;

namespace TwitchLeonScript.Core.App.Commands.MemeBonus
{
    public sealed class SendMemeBonusHandler(
        MemeBonusService memeBonusService)
        : IRequestHandler<SendMemeBonusCommand, SendMemeBonusResponse>
    {
        private readonly MemeBonusService _memeBonusService = memeBonusService;

        public async Task<SendMemeBonusResponse> Handle(SendMemeBonusCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);
            ArgumentNullException.ThrowIfNull(request.MemeBonus);

            var statusCode = await _memeBonusService.SendGivePointsAsync(request.OAuthToken, request.MemeBonus);

            return new SendMemeBonusResponse
            { 
                IsSuccess = statusCode == HttpStatusCode.OK,
                StatusCode = statusCode,
            };
        }
    }
}
