using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NotificationService.Infraestructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly MailConfig _config;
        private readonly ISendGridClient _sendGridClient;

        public NotificationService(MailConfig config, ISendGridClient sendGridClient)
        {
            _config = config;
            _sendGridClient = sendGridClient;
        }
        public async Task SendAsync(string subject, string content, string toEmail, string toName)
        {
            var from = new EmailAddress(_config.FromEmail, _config.FromName);
            var to = new EmailAddress(toEmail, toName);

            var message = new SendGridMessage
            {
                From = from,
                Subject = subject
            };

            message.AddContent(MimeType.Html, content);
            message.AddTo(to);

            message.SetClickTracking(false, false);
            message.SetOpenTracking(false);
            message.SetGoogleAnalytics(false);
            message.SetSubscriptionTracking(false);

            await _sendGridClient.SendEmailAsync(message);
        }
    }
}