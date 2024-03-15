using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectName.Core.DTOs;

namespace ProjectName.Core.Services
{
    public interface INotificationService
    {
        void ProcessNotification(NotificationInfoDTO notificationInfoDTO);
    }
}