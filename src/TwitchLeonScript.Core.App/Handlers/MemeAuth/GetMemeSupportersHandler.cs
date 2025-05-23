using MediatR;
using TwitchLeonScript.Core.App.Commands.MemeAuth;
using TwitchLeonScript.Core.App.Responses.MemeAuth;
using TwitchLeonScript.Core.Meme.Services;

namespace TwitchLeonScript.Core.App.Handlers.Twitch
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
