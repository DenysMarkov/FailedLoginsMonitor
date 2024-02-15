using FailedLoginsMonitor.BL.Interfaces;
using Microsoft.Extensions.Logging;

namespace FailedLoginsMonitor.BL.Services
{
    /// <summary>
    /// Implementation of a notification service via Telegram
    /// </summary>
    public class TelegramNotificationService : INotificationService
    {
        private readonly ILogger<TelegramNotificationService> _logger;
        private readonly string _botToken;
        private readonly string _chatId;

        public TelegramNotificationService(ILogger<TelegramNotificationService> logger, string botToken, string chatId)
        {
            _logger = logger;
            _botToken = botToken;
            _chatId = chatId;
        }

        public async Task SendNotification(string message)
        {
            using var client = new HttpClient();
            string url = $"https://api.telegram.org/bot{_botToken}/sendMessage";

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("chat_id", _chatId),
                new KeyValuePair<string, string>("text", message)
            });

            try
            {
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning($"Error sending message to Telegram: {response.StatusCode}");
                    _logger.LogWarning("Response content: " + responseContent);
                }
                else
                {
                    _logger.LogInformation("Notification sent to Telegram.");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
            }
        }
    }
}
