using Microsoft.AspNetCore.Mvc;
using PizzeriaBravo.OrderService.DataAccess.Entities;
using PizzeriaBravo.OrderService.DataAccess.Enums;
using PizzeriaBravo.OrderService.DataAccess.Repositories;

namespace PizzeriaBravo.OrderService.API.Extensions;

public static class OrderEndpointExtension
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/orders");

        group.MapGet("/", GetAllOrders);
        group.MapGet("/{id}", GetOrderById);
        group.MapGet("/customer/{id}", GetOrdersByCustomerId);
        group.MapGet("/status/{status}", GetOrdersByStatus);
        group.MapPost("/", CreateOrder);
        group.MapPut("/{id}/status/{status}", UpdateOrderStatus);
        group.MapDelete("/{id}", DeleteOrder);

        return app;
    }

    private static async Task<IResult> GetAllOrders([FromServices] OrderRepository repo)
    {
        var orders = await repo.GetAllOrdersAsync();
        return Results.Ok(orders);
    }

    private static async Task<IResult> GetOrderById([FromServices] OrderRepository repo, Guid id)
    {
        var order = await repo.GetOrderByIdAsync(id);
        return order is null ? Results.NotFound() : Results.Ok(order);
    }

    private static async Task<IResult> GetOrdersByCustomerId([FromServices] OrderRepository repo, Guid id)
    {
        var orders = await repo.GetOrdersByCustomerIdAsync(id);
        return Results.Ok(orders);
    }

    private static async Task<IResult> GetOrdersByStatus([FromServices] OrderRepository repo, OrderStatus status)
    {
        var orders = await repo.GetOrdersByOrderStatusAsync(status);
        return Results.Ok(orders);
    }

    private static async Task<IResult> CreateOrder([FromServices] OrderRepository repo, [FromBody] Order order)
    {
        var newOrder = await repo.CreateOrderAsync(order);
        return Results.Created($"/api/orders/{newOrder.Id}", newOrder);
    }

    private static async Task<IResult> UpdateOrderStatus([FromServices] OrderRepository repo, Guid id, OrderStatus status)
    {
        var order = await repo.UpdateOrderStatusAsync(id, status);
        return order is null ? Results.NotFound() : Results.Ok(order);
    }

    private static async Task<IResult> DeleteOrder([FromServices] OrderRepository repo, Guid id)
    {
        await repo.DeleteOrderAsync(id);
        return Results.Ok(id);
    }
}
