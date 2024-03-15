using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationService.DTOs;

namespace NotificationService.Infraestructure.Repositories
{
    public interface IMailRepository
    {
        Task<EmailTemplateDto> GetTemplate(string @event);
    }
}