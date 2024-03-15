namespace NotificationService.Infraestructure.Repositories

{
    public class MongoDbOptions
    {
        public string? ConnectionString { get; set; }
        public string? Database { get; set; }
    }
}