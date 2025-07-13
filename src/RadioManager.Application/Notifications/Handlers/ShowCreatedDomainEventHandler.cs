using MediatR;
using RadioManager.Application.Common.Notifications;
using RadioManager.Domain.Shows.Events;

namespace RadioManager.Application.Notifications.Handlers
{
    internal sealed class ShowCreatedDomainEventHandler(
        INotificationSender notificationSender) : INotificationHandler<ShowCreatedEvent>
    {
        public async Task Handle(ShowCreatedEvent notification, CancellationToken cancellationToken)
        {
            await notificationSender.SendShowCreatedAsync(notification.Title,
                notification.StartTime, notification.Presenter);
        }
    }
}
