using MediatR;
using MemeAlertsScript.Core.App.Commands.Bonus;
using MemeAlertsScript.Core.App.Responses.Bonus;
using MemeAlertsScript.Core.Meme.Services;

namespace MemeAlertsScript.Core.App.Handlers.Bonus
{
    public class SendMemeBonusHandler(
        MemeBonusService memeBonusService)
        : IRequestHandler<SendMemeBonusCommand, SendMemeBonusResponse>
    {
        private readonly MemeBonusService _memeBonusService = memeBonusService;

        public async Task<SendMemeBonusResponse> Handle(SendMemeBonusCommand request, CancellationToken cancellationToken)
        {
            var isSuccessStatusCode = await _memeBonusService.SendGivePointsAsync(
                accessToken: request.AccessToken,
                userId: request.UserId,
                streamerId: request.StreamerId,
                value: request.Value);

            return new SendMemeBonusResponse
            {
                IsSuccessStatusCode = isSuccessStatusCode
            };
        }
    }
}
