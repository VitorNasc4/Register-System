using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProjectName.Core.DTOs;
using ProjectName.Core.Services;

namespace ProjectName.Infrastructure.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Notifications";
        public NotificationService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }
        public void ProcessNotification(NotificationInfoDTO notificationInfoDTO)
        {
            var notificationInfoJson = JsonSerializer.Serialize(notificationInfoDTO);
            var notificationInfoBytes = Encoding.UTF8.GetBytes(notificationInfoJson);

            _messageBusService.Publish(QUEUE_NAME, notificationInfoBytes);
        }
    }
}