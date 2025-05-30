using MediatR;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Domain.Abstractions;

namespace TwitchLeonScript.Application.Queries.MemeState
{
    public sealed class GetMemeStateHandler(
        IMemeStateService stateService)
        : IRequestHandler<GetMemeStateQuery, Result<GetMemeStateResponse>>
    {
        public Task<Result<GetMemeStateResponse>> Handle(GetMemeStateQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.Json);

            try
            {
                return Task.FromResult(Result<GetMemeStateResponse>.Success(new GetMemeStateResponse
                {
                    IsCsIaAuth = stateService.IsCsIaAuth(request.Json),
                }));
            }
            catch (Exception ex)
            {
                return Task.FromResult(Result<GetMemeStateResponse>.Failure(ex.Message));
            }
        }
    }
}
