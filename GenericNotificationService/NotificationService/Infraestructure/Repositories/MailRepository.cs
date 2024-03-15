using MongoDB.Driver;
using NotificationService.DTOs;
using NotificationService.Infraestructure.Repositories;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Repositories
{
    public class MailRepository : IMailRepository
    {
        private readonly IMongoCollection<EmailTemplateDto> _collection;

        public MailRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<EmailTemplateDto>("email-templates");
        }

        public async Task<EmailTemplateDto> GetTemplate(string @event)
        {
            try
            {
                var template = await _collection.Find(c => c.Event == @event).SingleOrDefaultAsync();
                return template;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new EmailTemplateDto();
            }
        }
    }
}
