using System.Diagnostics.Eventing.Reader;
using FailedLoginsMonitor.BL.Interfaces;

namespace FailedLoginsMonitor.BL
{
    /// <summary>
    /// Implementing Security Event Monitoring
    /// </summary>
    public class SecurityEventMonitor : IEventMonitor
    {
        private readonly ILogParser _logParser;
        private readonly INotificationService _notificationService;

        public SecurityEventMonitor(ILogParser logParser, INotificationService notificationService)
        {
            _logParser = logParser;
            _notificationService = notificationService;
        }

        public void StartMonitoring()
        {
            var query = new EventLogQuery("Security", PathType.LogName, "*[System/EventID=4625]"); // Event ID 4625 - failed login attempt
            var watcher = new EventLogWatcher(query);

            watcher.EventRecordWritten += async (sender, eventArgs) =>
            {
                if (eventArgs.EventRecord != null)
                {
                    var message = _logParser.Parse(eventArgs.EventRecord);
                    Console.WriteLine("Failed login detected: " + message);
                    await _notificationService.SendNotification(message);
                }
            };

            watcher.Enabled = true;
            Console.WriteLine("Monitoring failed login attempts. Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
