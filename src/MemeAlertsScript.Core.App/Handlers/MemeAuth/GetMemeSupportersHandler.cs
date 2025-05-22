using MediatR;
using MemeAlertsScript.Core.App.Commands.MemeAuth;
using MemeAlertsScript.Core.App.Responses.MemeAuth;
using MemeAlertsScript.Core.Meme.Services;

namespace MemeAlertsScript.Core.App.Handlers.Twitch
{
    public class GetMemeSupportersHandler(
        MemeSupporterService memeSupporterService)
        : IRequestHandler<GetMemeSupportersCommand, GetMemeSupportersResponse>
    {
        public readonly MemeSupporterService _memeSupporterService = memeSupporterService;

        public async Task<GetMemeSupportersResponse> Handle(GetMemeSupportersCommand request, CancellationToken cancellationToken)
        {
            var supporters = await _memeSupporterService.GetSupportersAsync(request.OAuthToken);

            return new GetMemeSupportersResponse
            {
                Supporters = supporters
            };
        }
    }
}
