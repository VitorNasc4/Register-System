using System;
using System.Text;
using ProjectName.API.Filters;
using ProjectName.Application.Validators;
using ProjectName.Core.Repositories;
using ProjectName.Core.Services;
using ProjectName.Infrastructure.AuthServices;
using ProjectName.Infrastructure.Persistence;
using ProjectName.Infrastructure.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectName.Application.Commands.UserCommands.CreateUser;
using ProjectName.Infrastructure.MessageBus;
using ProjectName.Infrastructure.NotificationService;

var builder = WebApplication.CreateBuilder(args);

// var connectionString = builder.Configuration.GetConnectionString("ProjectNameCs");
// Adicionando a configuração do DbContext com SQL Server
// builder.Services.AddDbContext<ProjectNameDbContext>(options => options.UseSqlServer(connectionString));

// Para usar o banco de dados em memória (caso necessário), descomente a linha abaixo e comente a de cima
//builder.Services.AddDbContext<ProjectNameDbContext>(options => options.UseInMemoryDatabase("ProjectName"));

// Para usar o Postgres
var connectionString = builder.Configuration.GetConnectionString("ProjectNameCsPostgres");
builder.Services.AddDbContext<ProjectNameDbContext>
    (option => option.UseNpgsql(connectionString));

// Injeções de dependências
builder.Services.AddMediatR(typeof(CreateUserCommand));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMessageBusService, MessageBusService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Ajustando HttpClient
builder.Services.AddHttpClient();


builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilters)));

// Fluent Validator
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

// Autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectName.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Aplica as migrations automaticamente ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProjectNameDbContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectName.API v1"));

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
