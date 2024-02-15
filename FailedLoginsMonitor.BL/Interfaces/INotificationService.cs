namespace FailedLoginsMonitor.BL.Interfaces
{
    /// <summary>
    /// Interface for notification service
    /// </summary>
    public interface INotificationService
    {
        Task SendNotification(string message);
    }
}
