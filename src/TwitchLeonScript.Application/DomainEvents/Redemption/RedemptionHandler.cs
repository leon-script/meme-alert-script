using MediatR;
using TwitchLeonScript.Application.Extensions;
using TwitchLeonScript.Domain.Events;

namespace TwitchLeonScript.Application.DomainEvents.Redemption
{
    public sealed class RedemptionHandler(IMediator mediator) : INotificationHandler<RedemptionReceived>
    {
        public async Task Handle(RedemptionReceived notification, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(notification);
            ArgumentNullException.ThrowIfNull(notification.Redemption);

            try
            {
                if (notification.Redemption.Reward.Prompt.HasMemeTag())
                {
                    await mediator.Publish(new MemeRedemptionReceived
                    {
                        Redemption = notification.Redemption
                    }, cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                await mediator.Publish(new RedemptionErrorHandling
                {
                    Reason = ex.Message,
                    Redemption = notification.Redemption
                }, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
