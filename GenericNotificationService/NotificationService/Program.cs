using MongoDB.Bson;
using MongoDB.Driver;
using NotificationService.Consumer;
using NotificationService.Infraestructure.Repositories;
using NotificationService.Infraestructure.Services;
using NotificationService.Infrastructure.Repositories;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var services = builder.Services;

// Add Consumer
services.AddHostedService<NotificationConsumer>();

// Add Mail Service
var config = new MailConfig();
configuration.GetSection("Notifications").Bind(config);
services.AddSingleton<MailConfig>(m => config);
services.AddSendGrid(sp => sp.ApiKey = config.SendGridApiKey);
services.AddTransient<INotificationService, NotificationService.Infraestructure.Services.NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

