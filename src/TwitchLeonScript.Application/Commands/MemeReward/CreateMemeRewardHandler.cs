using MediatR;
using TwitchLeonScript.Application.Common;
using TwitchLeonScript.Application.Extensions;
using TwitchLeonScript.Domain.Abstractions;

namespace TwitchLeonScript.Application.Commands.MemeReward
{
    public class CreateMemeRewardHandler(
        ITwitchRewardApiService twitchRewardApiService)
        : IRequestHandler<CreateMemeRewardCommand, Result<CreateMemeRewardResponse>>
    {
        public async Task<Result<CreateMemeRewardResponse>> Handle(CreateMemeRewardCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(request.Title);
            ArgumentNullException.ThrowIfNull(request.TwitchCost);
            ArgumentNullException.ThrowIfNull(request.MemeCost);
            ArgumentNullException.ThrowIfNull(request.Prompt);

            try
            {
                var reward = await twitchRewardApiService.CreateRewardAsync(
                    title: request.Title,
                    cost: request.TwitchCost.Value,
                    prompt: request.Prompt + "\n" + request.MemeCost.Value.ToMemeTag()).ConfigureAwait(false);

                return Result<CreateMemeRewardResponse>.Success(new CreateMemeRewardResponse { Reward = reward });
            }
            catch (Exception ex)
            {
                return Result<CreateMemeRewardResponse>.Failure(ex.Message);
            }
        }
    }
}
