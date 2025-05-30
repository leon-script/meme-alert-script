using MediatR;
using TwitchLeonScript.Application.Extensions;
using TwitchLeonScript.Domain.Abstractions;
using TwitchLeonScript.Domain.Enums;
using TwitchLeonScript.Domain.Events;

namespace TwitchLeonScript.Application.DomainEvents.Redemption
{
    public sealed class MemeRedemptionHandler(
        IMemeSupporterApiService memeSupporterApiService,
        ITwitchRedemptionApiService twitchRedemptionApiService,
        IMemeBonusApiService memeBonusApiService,
        IMediator mediator)
        : INotificationHandler<MemeRedemptionReceived>
    {
        public async Task Handle(MemeRedemptionReceived notification, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(notification);
            ArgumentNullException.ThrowIfNull(notification.Redemption);
            ArgumentNullException.ThrowIfNull(notification.Redemption.Input);

            try
            {
                var supporters = await memeSupporterApiService.GetSupportersAsync().ConfigureAwait(false);
                var memeSupporter = supporters.FirstOrDefault(x => x.Name.Equals(notification.Redemption.Input));

                if (memeSupporter is null)
                {
                    await twitchRedemptionApiService.UpdateRedemptionStatusAsync(
                        broadcasterId: notification.Redemption.Broadcaster.Id,
                        rewardId: notification.Redemption.Reward.Id,
                        redemptionId: notification.Redemption.Id,
                        status: RedemptionStatus.Canceled).ConfigureAwait(false);

                    await mediator.Publish(new MemeSupporterNotFound
                    {
                        Redemption = notification.Redemption
                    }, cancellationToken).ConfigureAwait(false);

                    // CHAT: Send message to chat about meme supporter not found

                    return;
                }

                await memeBonusApiService.GivePointsAsync(
                    userId: memeSupporter.Id,
                    value: notification.Redemption.Reward.Prompt.ParseMemeTagNumber()).ConfigureAwait(false);

                await twitchRedemptionApiService.UpdateRedemptionStatusAsync(
                    broadcasterId: notification.Redemption.Broadcaster.Id,
                    rewardId: notification.Redemption.Reward.Id,
                    redemptionId: notification.Redemption.Id,
                    status: RedemptionStatus.Fulfilled).ConfigureAwait(false);

                await mediator.Publish(new MemeRedemptionResolved
                {
                    Redemption = notification.Redemption
                }, cancellationToken).ConfigureAwait(false);

                // CHAT: Send message to chat about meme supporter redemption received
            }
            catch (Exception ex)
            {
                //await twitchRedemptionApiService.UpdateRedemptionStatusAsync(
                //    broadcasterId: notification.Redemption.Broadcaster.Id,
                //    rewardId: notification.Redemption.Reward.Id,
                //    redemptionId: notification.Redemption.Id,
                //    status: RedemptionStatus.Canceled).ConfigureAwait(false);

                await mediator.Publish(new MemeRedemptionErrorHandling
                {
                    Reason = ex.Message,
                    Redemption = notification.Redemption
                }, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
