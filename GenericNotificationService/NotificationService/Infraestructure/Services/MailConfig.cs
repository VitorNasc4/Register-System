namespace NotificationService.Infraestructure.Services

{
    public class MailConfig
    {
        public string? SendGridApiKey { get; set; }
        public string? FromName { get; set; }
        public string? FromEmail { get; set; }
    }
}