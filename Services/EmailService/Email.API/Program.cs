using Email.Application.EventHandlers;
using Email.Infrastructure.Options;
using Email.Infrastructure.Services;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using Shared.Application.Interfaces;
using Shared.RabbitMQ.Abstractions;
using Shared.RabbitMQ.Events.Auth;
using Shared.RabbitMQ.Extensions;
using MediatR;
using System.Reflection;
using Email.Application.Commands.SendEmail;
using Email.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(SendEmailCommand).Assembly);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEventBus(builder.Configuration);
builder.Services.AddSingleton<UserRegisteredEventHandler>();
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddSingleton<IEmailSender>(sp =>
{
    var options = sp.GetRequiredService<IOptions<SmtpOptions>>().Value;
    return new SmtpEmailSender(options.Host, options.Port, options.User, options.Password);
});
var app = builder.Build();

using var scope = app.Services.CreateScope();
var bus = scope.ServiceProvider.GetRequiredService<IEventBus>();
await bus.PurgeQueueAsync<UserRegisteredEvent>();
var handler = scope.ServiceProvider.GetRequiredService<UserRegisteredEventHandler>();
await bus.Subscribe<UserRegisteredEvent>(handler.Handle);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
