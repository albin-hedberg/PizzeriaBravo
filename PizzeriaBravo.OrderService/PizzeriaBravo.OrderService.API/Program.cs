using MongoDB.Driver;
using PizzeriaBravo.OrderService.API.Extensions;
using PizzeriaBravo.OrderService.API.Interfaces;
using PizzeriaBravo.OrderService.API.Services;
using PizzeriaBravo.OrderService.DataAccess.Entities;
using PizzeriaBravo.OrderService.DataAccess.Interfaces;
using PizzeriaBravo.OrderService.DataAccess.Repositories;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderService<Order>, OrderRepository>();
builder.Services.AddSingleton<IMessageService, RabbitMqService>();
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>(_ => new()
{
    Uri = new Uri(builder.Configuration["RabbitMq:MqUri"] ?? string.Empty),
    ClientProvidedName = builder.Configuration["RabbitMq:ClientProvidedName"] ?? string.Empty,
    HostName = builder.Configuration["RabbitMq:HostName"] ?? string.Empty,
    UserName = builder.Configuration["RabbitMq:UserName"] ?? string.Empty,
    Password = builder.Configuration["RabbitMq:Password"] ?? string.Empty
});

var app = builder.Build();

app.MapOrderEndpoints();

app.Run();
