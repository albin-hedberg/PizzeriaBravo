using MongoDB.Driver;
using PizzeriaBravo.OrderService.API.Extensions;
using PizzeriaBravo.OrderService.DataAccess.Entities;
using PizzeriaBravo.OrderService.DataAccess.Interfaces;
using PizzeriaBravo.OrderService.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));

builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<IOrderService<Order>, OrderRepository>();

var app = builder.Build();

app.MapOrderEndpoints();

app.Run();
