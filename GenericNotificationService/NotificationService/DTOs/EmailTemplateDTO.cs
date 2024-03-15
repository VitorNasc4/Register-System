using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.DTOs
{
    public class EmailTemplateDto
    {
        public Guid Id { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public string? Event { get; set; }
    }
}