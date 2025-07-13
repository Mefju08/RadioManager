using RadioManager.Application.Common.Notifications;

namespace RadioManager.Infrastructure.Notifications
{
    internal sealed class EmailNotificationSender : INotificationSender
    {
        public Task SendShowCreatedAsync(string title, DateTime startTime, string presenter)
        {
            Console.WriteLine($"Nowa audycja: {title} o {startTime} prowadzona przez {presenter}");
            return Task.CompletedTask;
        }
    }
}
