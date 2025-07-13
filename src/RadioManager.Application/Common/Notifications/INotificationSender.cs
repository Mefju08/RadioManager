namespace RadioManager.Application.Common.Notifications
{
    public interface INotificationSender
    {
        Task SendShowCreatedAsync(string title, DateTime startTime, string presenter);
    }
}
