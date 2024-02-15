using FailedLoginsMonitor.BL.Interfaces;

namespace FailedLoginsMonitor.BL
{
    /// <summary>
    /// Implementation of the Composite pattern to combine multiple notification services
    /// </summary>
    public class CompositeNotificationService : INotificationService
    {
        private readonly List<INotificationService> _notificationServices;

        public CompositeNotificationService(IEnumerable<INotificationService> notificationServices)
        {
            _notificationServices = new List<INotificationService>(notificationServices);
        }

        public async Task SendNotification(string message)
        {
            foreach (var service in _notificationServices)
            {
                await service.SendNotification(message);
            }
        }
    }
}
