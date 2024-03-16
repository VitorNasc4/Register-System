using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using NotificationService.DTOs;
using NotificationService.Infraestructure.Repositories;
using NotificationService.Infraestructure.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.Consumer
{
    public class NotificationConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private const string QUEUE = "Notifications";

        public NotificationConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            // var factory = new ConnectionFactory
            // {
            //     HostName = "rabbitmq",
            //     UserName = "guest",
            //     Password = "guest"
            // };
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: QUEUE,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();
                var notificationInfoJson = Encoding.UTF8.GetString(byteArray);
                var notificationInfoDTO = JsonSerializer.Deserialize<NotificationInfoDTO>(notificationInfoJson);

                if (notificationInfoDTO is not null)
                {
                    await SendEmail(notificationInfoDTO);
                }

                _channel.BasicAck(eventArgs.DeliveryTag, false);

            };

            _channel.BasicConsume(QUEUE, false, consumer);

            return Task.CompletedTask;
        }
        private async Task SendEmail(NotificationInfoDTO notificationInfoDTO)
        {
            var template = new EmailTemplateDto
            {
                Subject = "Welcome, {0}!",
                Content = "Welcome to AwesomeShop, {0}! You can search our products in awesome-shop-dot-com,",
                Event = "CustomerCreated"
            };

            var subject = string.Format(template.Subject!, notificationInfoDTO.FullName);
            var content = string.Format(template.Content!, notificationInfoDTO.FullName);

            var outlook = new Email("smtp.office365.com", "SeuEmailOutlook", "SuaSenhaDoEmail");
            outlook.SendEmail(
                emailTo: notificationInfoDTO.Email,
                subject: subject,
                body: content
            );

            await Task.CompletedTask;
            Console.WriteLine("Email enviado com sucesso");
        }
    }
}