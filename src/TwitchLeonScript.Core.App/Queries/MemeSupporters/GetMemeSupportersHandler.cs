using MediatR;
using TwitchLeonScript.Core.Meme.Services;

namespace TwitchLeonScript.Core.App.Queries.MemeSupporters
{
    public sealed class GetMemeSupportersHandler(
        MemeSupporterService memeSupporterService)
        : IRequestHandler<GetMemeSupportersCommand, GetMemeSupportersResponse>
    {
        public readonly MemeSupporterService _memeSupporterService = memeSupporterService;

        public async Task<GetMemeSupportersResponse> Handle(GetMemeSupportersCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.OAuthToken);

            var supporters = await _memeSupporterService.GetSupportersAsync(request.OAuthToken);

            return new GetMemeSupportersResponse
            {
                Supporters = supporters
            };
        }
    }
}
