using FailedLoginsMonitor.BL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Uwp.Notifications;

namespace FailedLoginsMonitor.BL.Services
{
    /// <summary>
    /// Implementation of a notification service via showing Toast notifications on the Desktop
    /// </summary>
    public class ToastNotificationService : INotificationService
    {
        private readonly ILogger<ToastNotificationService> _logger;

        public ToastNotificationService(ILogger<ToastNotificationService> logger)
        {
            _logger = logger;
        }

        public async Task SendNotification(string message)
        {
            try
            {
                var toastContent = new ToastContentBuilder()
                    .AddText("Security Alert")
                    .AddText(message);

                toastContent.Show();

                _logger.LogInformation("Toast notification displayed.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error displaying toast notification: " + ex.Message);
            }

            await Task.CompletedTask;
        }
    }
}
