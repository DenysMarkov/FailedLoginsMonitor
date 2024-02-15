using System.Net.Mail;
using FailedLoginsMonitor.BL.Interfaces;
using Microsoft.Extensions.Logging;

namespace FailedLoginsMonitor.BL.Services
{
    /// <summary>
    /// Implementation of a notification service via e-mail
    /// </summary>
    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _toEmail;

        public EmailNotificationService(ILogger<EmailNotificationService> logger, string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string fromEmail, string toEmail)
        {
            _logger = logger;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _fromEmail = fromEmail;
            _toEmail = toEmail;
        }

        public async Task SendNotification(string message)
        {
            using var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new System.Net.NetworkCredential(_smtpUsername, _smtpPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage(_fromEmail, _toEmail)
            {
                Subject = "Security Alert",
                Body = message
            };

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Notification sent to email.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error sending email: " + ex.Message);
            }
        }
    }
}
